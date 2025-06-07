from enum import Enum
from typing import List, Dict, Any
from odps import ODPS
from dotenv import load_dotenv
from pathlib import Path
import os
import json
import pandas as pd

def load_partition_config() -> Dict[str, List[str]]:
    """Load partition configuration from config file
    
    Returns:
        Dictionary containing partition fields and corresponding table lists, 
        formatted as {'partition_field': ['table1', 'table2']}
    """
    config_path = Path(__file__).parent.parent / "config.json"
    try:
        with open(config_path, 'r') as f:
            config = json.load(f)
            return config.get('partition_config', {})
    except Exception as e:
        print(f"Error loading partition config: {e}")
        return {}

class SourceType(Enum):
    OLD = "old"
    NEW = "new"

class OdpsFetcher:
    def __init__(self, source_type: SourceType):
        """Initialize ODPS fetcher with specified source type"""
        load_dotenv()
        
        # Get connection info based on source type
        suffix = source_type.value.upper()
        self.access_key = os.getenv(f"{suffix}_DATAWORKS_ACCESS_KEY")
        self.access_secret = os.getenv(f"{suffix}_DATAWORKS_ACCESS_SECRET")
        self.project = os.getenv(f"{suffix}_DATAWORKS_PROJECT")
        self.endpoint = os.getenv(f"{suffix}_DATAWORKS_ENDPOINT")
        
        if not all([self.access_key, self.access_secret, self.project, self.endpoint]):
            raise ValueError(f"Missing required ODPS configuration for {source_type.value} source")
        
        self.odps = ODPS(
            access_id=self.access_key,
            secret_access_key=self.access_secret,
            project=self.project,
            endpoint=self.endpoint
        )
        
        # Load partition table configuration
        self.partition_config = load_partition_config()

    def get_partition_field(self, table_name: str) -> str:
        """Get the partition field for a table"""
        for field, tables in self.partition_config.items():
            if table_name in tables:
                return field
        return ''

    def is_partitioned_table(self, table_name: str) -> bool:
        """Check if a table is partitioned"""
        return bool(self.get_partition_field(table_name))

    def get_table_partition_condition(self, table_name: str) -> str:
        """Get the partition condition for a table"""
        partition_field = self.get_partition_field(table_name)
        if partition_field:
            return f"where {partition_field} = max_pt('{table_name}')"
        return ""

    def fetch_data(self, sql: str) -> List[Dict[str, Any]]:
        """
        Execute SQL query and return results
        
        Args:
            sql: SQL query to execute
            
        Returns:
            List of dictionaries containing the query results
        """
        instance = self.odps.execute_sql(sql)
        with instance.open_reader() as reader:
            return [dict(row) for row in reader]
            
    def fetch_df(self, sql: str) -> "pd.DataFrame":
        """
        Execute SQL query and return results as pandas DataFrame
        
        Args:
            sql: SQL query to execute
            
        Returns:
            pandas DataFrame containing the query results
        """
        instance = self.odps.execute_sql(sql)
        with instance.open_reader() as reader:
            return reader.to_pandas()

from typing import Tuple
from datetime import datetime
from loguru import logger
from odps_fetcher import OdpsFetcher

class TableComparator:
    def __init__(self, old_fetcher: OdpsFetcher, new_fetcher: OdpsFetcher):
        """Initialize table comparator with fetchers for both data sources"""
        self.old_fetcher = old_fetcher
        self.new_fetcher = new_fetcher
        self.comparison_results = []

    def compare_table_count(self, table_name: str) -> Tuple[int, int, float, float]:
        """
        Compare the total number of records in a table between old and new sources
        
        Args:
            table_name: The name of the table to compare
            
        Returns:
            Tuple containing:
            - count in old source
            - count in new source
            - difference percentage ((new - old) / old * 100)
        """
        # Get partition condition for the table
        partition_condition = self.old_fetcher.get_table_partition_condition(table_name)
        
        # Prepare count queries with partition condition
        count_sql = f"SELECT COUNT(*) as total FROM {table_name} {partition_condition}"
        logger.debug(f"Executing query: {count_sql}")
        
        # Get counts from both sources
        old_count = self.old_fetcher.fetch_df(count_sql).iloc[0]['total']
        new_count = self.new_fetcher.fetch_df(count_sql).iloc[0]['total']
        
        # Calculate differences
        diff_absolute = new_count - old_count
        diff_percentage = (diff_absolute / old_count * 100) if old_count != 0 else float('inf')
        
        # Store result
        compare_time = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        self.comparison_results.append({
            'compare_time': compare_time,
            'table_name': table_name,
            'old_count': old_count,
            'new_count': new_count,
            'diff_absolute': diff_absolute,
            'diff_percentage': diff_percentage
        })
        
        # Log any difference
        if diff_absolute != 0:
            logger.warning(f"Table {table_name} has {diff_percentage:+.2f}% difference ({diff_absolute:+,} records)")
        
        return old_count, new_count, diff_absolute, diff_percentage

    def format_comparison_result(self, table_name: str) -> str:
        """
        Get a formatted string of the comparison result
        
        Args:
            table_name: The name of the table to compare
            
        Returns:
            A formatted string showing the comparison result
        """
        try:
            old_count, new_count, diff_absolute, diff_percentage = self.compare_table_count(table_name)
            result = (
                f"Table: {table_name}\n"
                f"Old Source Count: {old_count:,}\n"
                f"New Source Count: {new_count:,}\n"
                f"Difference: {diff_absolute:+,} records ({diff_percentage:+.2f}%)"
            )
            return result
        except Exception as e:
            error_msg = f"Error comparing table {table_name}: {str(e)}"
            logger.error(error_msg)
            self.comparison_results.append({
                'compare_time': datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
                'table_name': table_name,
                'old_count': None,
                'new_count': None,
                'diff_absolute': None,
                'diff_percentage': None,
                'error': str(e)
            })
            return error_msg



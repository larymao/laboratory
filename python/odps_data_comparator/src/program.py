import sys
from pathlib import Path
from loguru import logger
from comparison_service import ComparisonService

def setup_logger():
    """Configure loguru logger with console and file handlers"""
    # Remove default handler
    logger.remove()
    
    # Add console handler with custom format
    logger.add(
        sys.stderr,
        format="<green>{time:YYYY-MM-DD HH:mm:ss}</green> | <level>{level: <8}</level> | <cyan>{name}</cyan>:<cyan>{function}</cyan>:<cyan>{line}</cyan> - <level>{message}</level>",
        level="INFO"
    )
    
    # Add file handler
    log_path = Path(__file__).parent.parent / "logs"
    log_path.mkdir(exist_ok=True)
    logger.add(
        log_path / "odps_comparator_{time:YYYY-MM-DD}.log",
        rotation="00:00",  # Create new file at midnight
        retention="30 days",  # Keep logs for 30 days
        format="{time:YYYY-MM-DD HH:mm:ss} | {level: <8} | {name}:{function}:{line} - {message}",
        level="DEBUG"
    )

def main():
    # Setup logging
    setup_logger()
    logger.info("Starting ODPS data comparison")
    
    # Load configuration
    tables = ComparisonService.load_tables_config()
    if not tables:
        logger.info("Comparison stopped: No tables to compare")
        return
    
    # Initialize and run comparisons
    comparator = ComparisonService.initialize_comparator()
    ComparisonService.compare_tables(comparator, tables)
    
    # Export results
    ComparisonService.export_results_to_excel(comparator)

if __name__ == "__main__":
    main()
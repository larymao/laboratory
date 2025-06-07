# ODPS Data Comparator Makefile

# Detect operating system
ifeq ($(OS),Windows_NT)
	PYTHON = ./.venv/Scripts/python.exe
	VENV_ACTIVATE = ./.venv/Scripts/activate
	RMDIR = powershell -Command "Remove-Item -Force -Recurse -ErrorAction Ignore"
	RMFILE = powershell -Command "Remove-Item -Force -ErrorAction Ignore"
else
	PYTHON = ./.venv/bin/python
	VENV_ACTIVATE = source ./.venv/bin/activate
	RMDIR = rm -rf
	RMFILE = rm -f
endif

.PHONY: help venv install clean run activate lint test

# Default target, show help information
help:
ifeq ($(OS),Windows_NT)
	@echo Available commands:
	@echo   make venv      - Create Python virtual environment
	@echo   make install   - Install dependencies using uv
	@echo   make clean     - Clean up generated files
	@echo   make run       - Run the ODPS data comparator
	@echo   make activate  - Print command to activate virtual environment (for development)
	@echo.
	@echo Normal usage:
	@echo   make venv ^&^& make install  - First time setup
	@echo   make run                   - Run the program
	@echo.
	@echo For development:
	@echo   make activate              - Show how to activate venv for development
else
	@echo "Available commands:"
	@echo "  make venv      - Create Python virtual environment"
	@echo "  make install   - Install dependencies using uv"
	@echo "  make clean     - Clean up generated files"
	@echo "  make run       - Run the transfer checker"
	@echo "  make activate  - Print command to activate virtual environment (for development)"
	@echo
	@echo "Normal usage:"
	@echo "  make venv && make install  - First time setup"
	@echo "  make run                   - Run the program"
	@echo
	@echo "For development:"
	@echo "  make activate              - Show how to activate venv for development"
endif

# Create virtual environment
venv:
	@echo "Creating virtual environment..."
	python -m venv .venv
	@echo "Virtual environment created. Run 'make activate' to see activation command."

# Install dependencies
install:
	@echo "Installing dependencies using uv..."
ifeq ($(OS),Windows_NT)
	@powershell -Command ". .\.venv\Scripts\Activate.ps1; python -m pip install uv; python -m uv pip install -r requirements.txt"
else
	@bash -c "source ./.venv/bin/activate && python -m pip install uv && python -m uv pip install -r requirements.txt"
endif

# Clean up project
clean:
	@echo "Cleaning up..."
	$(RMDIR) .venv
	$(RMDIR) reports
	$(RMDIR) logs
	$(RMDIR) "src/__pycache__"
	$(RMFILE) src/*.pyc
	@echo "Cleanup complete."

# Run project
run:
	@echo "Running ODPS data comparator..."
ifeq ($(OS),Windows_NT)
	@powershell -Command ". .\.venv\Scripts\Activate.ps1; python src/program.py"
else
	@bash -c "source ./.venv/bin/activate && python src/program.py"
endif

# Print activation command
activate:
	@echo "To activate the virtual environment, run:"
	@echo "$(VENV_ACTIVATE)"

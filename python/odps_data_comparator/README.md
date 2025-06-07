# ODPS 数据比对工具

用于比对不同 ODPS 数据源中表数据的一致性 (当前仅比对数据量的一致性)，支持分区表，自动生成差异报告。主要用于数据迁移、数据同步等场景的验证工作。

## 环境要求

- Python 3.11+
- make（Windows 需要安装 [GnuWin32](http://gnuwin32.sourceforge.net/packages/make.htm)）
- PowerShell (Windows) 或 Bash (Linux/macOS)
- 网络能访问 MaxCompute

## 配置说明

### 1. 数据源配置

在项目根目录创建 `.env` 文件：

```properties
# 旧数据源配置
OLD_DATAWORKS_ACCESS_KEY=your_old_access_key
OLD_DATAWORKS_ACCESS_SECRET=your_old_access_secret
OLD_DATAWORKS_PROJECT=your_old_project
OLD_DATAWORKS_ENDPOINT=your_old_endpoint

# 新数据源配置
NEW_DATAWORKS_ACCESS_KEY=your_new_access_key
NEW_DATAWORKS_ACCESS_SECRET=your_new_access_secret
NEW_DATAWORKS_PROJECT=your_new_project
NEW_DATAWORKS_ENDPOINT=your_new_endpoint
```

### 2. 表配置

编辑 `config.json` 文件：

```json
{
  "tables": [
    "table_with_dt_partition",
    "table_with_pt_partition",
    "table_without_partition"
  ],
  "partition_config": {
    "dt": [
      "table_with_dt_partition"
    ],
    "pt": [
      "table_with_pt_partition"
    ]
  }
}
```

配置说明：
- `tables`: 需要比对的表列表
- `partition_config`: 分区表配置
  - `dt`: 使用 `dt` 作为分区字段的表 (注意替换为真实的分区字段)
  - `pt`: 使用 `pt` 作为分区字段的表 (注意替换为真实的分区字段)
  - 未配置的表默认不使用分区

## 使用说明

### 首次使用

1. 创建虚拟环境：
   ```powershell
   make venv
   ```

2. 安装依赖：
   ```powershell
   make install
   ```

3. 运行比对：
   ```powershell
   make run
   ```

### 日常使用

直接运行：
```powershell
make run
```

### 清理项目

清理生成的文件（虚拟环境、报告、日志等）：
```powershell
make clean
```

### 所有可用命令

| 命令 | 说明 |
|------|------|
| `make help` | 显示帮助信息 |
| `make venv` | 创建虚拟环境 |
| `make install` | 安装依赖 |
| `make run` | 运行比对 |
| `make clean` | 清理文件 |
| `make activate` | 显示激活虚拟环境命令（开发用） |

## 输出说明

### 1. 控制台输出

实时显示每个表的比对结果：
- 新旧数据源的记录数
- 差异数量和百分比

### 2. Excel 报告

保存在 `reports` 目录：
- 文件名格式：`table_comparison_YYYYMMDD_HHMMSS.xlsx`
- 包含所有表的比对结果
- 有差异的行标记黄色背景

### 3. 日志文件

保存在 `logs` 目录：
- 文件名格式：`transfer_checker_YYYY-MM-DD.log`
- 记录详细的执行过程
- 包含错误和警告信息

## 常见问题

### 1. 环境相关

- **问题**: `make` 命令未找到  
  **解决**: Windows 需要安装 GnuWin32 的 make

- **问题**: 依赖安装失败  
  **解决**: 确保网络正常，必要时使用镜像源

### 2. 权限相关

- **问题**: 数据源连接失败  
  **解决**: 检查 `.env` 配置，确认网络和权限

### 3. 数据相关

- **问题**: 表未找到  
  **解决**: 检查表名拼写和大小写

- **问题**: 分区查询报错  
  **解决**: 检查分区配置是否正确

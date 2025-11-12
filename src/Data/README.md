# Data Layer - DDD Core Abstractions

这是一个**纯粹的** DDD（领域驱动设计）核心抽象层，专注于**最常用的模式**，**不依赖任何具体的 ORM 或数据库技术**。

## 🎯 设计理念

```
┌─────────────────────────────────────────┐
│   应用层 / 业务层                        │
│   (依赖 Data 抽象)                       │
└──────────────┬──────────────────────────┘
               │ 依赖
               ▼
┌─────────────────────────────────────────┐
│   Data (纯抽象层)                        │
│   - Domain 模型                          │
│   - Repository 接口                      │
│   - UnitOfWork 接口                      │
│   - Auditing 审计                        │
│   - Pagination 分页                      │
│   - 零外部依赖                           │
└─────────────────────────────────────────┘
               ▲ 实现
               │
┌──────────────┴──────────────────────────┐
│   具体实现（独立项目）                    │
│   - Data.EntityFramework                │
│   - Data.Dapper                         │
│   - Data.PostgreSQL                     │
└─────────────────────────────────────────┘
```

## ✨ 核心特性

- ✅ **零依赖设计** - 不依赖任何 ORM 或数据库
- ✅ **最大兼容性** - .NET Standard 2.0，支持所有 .NET 平台
- ✅ **简洁实用** - 只保留高频使用的核心功能
- ✅ **类型统一** - 所有 ID 默认为 `string`，所有时间为 `long`（Unix 秒）

## 📁 项目结构

```
Data/
├── Domain/                              # 领域模型
│   ├── Entity.cs                        # 实体基类
│   ├── AggregateRoot.cs                 # 聚合根基类
│   ├── IAggregateRoot.cs                # 聚合根接口
│   ├── ValueObject.cs                   # 值对象基类
│   └── DomainEvent.cs                   # 领域事件
│
├── Repositories/                        # 仓储
│   └── IRepository.cs                   # 仓储接口（简化）
│
├── UnitOfWork/                          # 工作单元
│   └── IUnitOfWork.cs                   # 工作单元接口
│
├── Auditing/                            # 审计
│   ├── IAuditable.cs                    # 审计接口
│   └── AuditedAggregateRoot.cs          # 审计聚合根
│
├── Pagination/                          # 分页
│   ├── IPagedList.cs                    # 分页列表接口
│   ├── PagedList.cs                     # 分页列表实现
│   └── PagedRequest.cs                  # 分页请求
│
├── Exceptions/                          # 异常
│   ├── DataException.cs                 # 数据异常基类
│   └── EntityNotFoundException.cs       # 实体未找到异常
│
├── Enums/                              # 枚举
│   └── SortOrder.cs                     # 排序方向
│
└── Extensions/                          # 扩展
    └── QueryableExtensions.cs           # IQueryable 扩展
```

## 📄 License

MIT License

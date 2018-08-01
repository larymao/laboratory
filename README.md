# Lary.Laboratory
**Lary.Laboratory**主要是[小醉魔](https://lary.me)同学的一个小小实验室，用于存放他日常编码中所积累的比较好玩的代码。目前主要包括的功能模块包括：  
- [x] Facebook周边（发布普通帖、广告帖，文件上传）  
- [x] Twitter的OAuth流程以及文件分片上传  
- [x] CRON表达式解析以及定时任务。  

## 小小实验室
**Lary.Laboratory**里的内容会随着小醉魔同学的日常开发不定时更新，如果你对共建这个小小实验室有兴趣，欢迎将你觉得有意思的功能模块汇入其中（请保持相对统一的编码/注释风格，感激不尽）。  

## 平台支持
* `.NET Core 2.1`

## 功能模块说明
各功能模块的具体说明文档放置于各自目录下，此处仅提供简单说明。
* `Samples` - 提供功能模块的样例代码。
* `Src` - 所有的功能模块都位于这个目录下。
  * `Src\Lary.Laboratory` - 目前来说并没什么用处，姑且放在一边。
  * `Src\Lary.Laboratory.Core` - 放置一些通用的基础方法。
* `Tests` - 为各个功能模块提供测试。

## 样例代码
`Samples`以及`Tests`目录下的东西，都可以愉快地作为样例代码进行查看。

## 编码风格
> 通过`StyleCop.Analyzer`进行样式控制，所有的约束都编写在工程根目录下的`Lary.Laboratory.ruleset`文件中。  
> 应用方法：修改项目`.csproj`文件，在`TargetFramework`下添加`<CodeAnalysisRuleSet>`节点，详情如下：
```xml
<PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <CodeAnalysisRuleSet>..\..\Lary.Laboratory.ruleset</CodeAnalysisRuleSet>
</PropertyGroup>
```

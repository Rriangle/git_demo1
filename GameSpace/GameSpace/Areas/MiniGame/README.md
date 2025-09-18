# MiniGame Area 配置說明

## 概述
此 Area 包含所有 MiniGame 相關的功能，包括前台和後台管理。

## 文件結構
`
Areas/MiniGame/
├── Controllers/          # 控制器
├── Models/              # 數據模型和 ViewModels
├── Views/               # 視圖文件
├── Services/            # 業務邏輯服務
├── Filters/             # 過濾器
└── config/              # 配置文件和擴展
`

## 配置使用

### 1. 在 Program.cs 中添加服務
`csharp
using GameSpace.Areas.MiniGame.config;

// 在 builder.Services 配置後添加
builder.Services.AddMiniGameServices(builder.Configuration);
`

### 2. 數據庫上下文
- 使用 GameSpace.Areas.MiniGame.Models.GameSpacedatabaseContext
- 包含所有必要的 DbSet 屬性
- 已配置所有模型關係

### 3. 服務註冊
- IMiniGameAdminService - 管理員服務
- IMiniGameAdminAuthService - 認證服務
- IMiniGameAdminGate - 權限控制

## 路由配置
確保在 Program.cs 中正確配置 Area 路由：

`csharp
// MiniGame Area 後台
app.MapControllerRoute(
    name: "minigame_admin",
    pattern: "MiniGame/Admin{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "MiniGame" });

// MiniGame Area 前台
app.MapControllerRoute(
    name: "minigame_area",
    pattern: "MiniGame/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "MiniGame" });
`

## 使用方式
1. 在控制器中注入 GameSpacedatabaseContext
2. 使用 IMiniGameAdminService 進行業務邏輯操作
3. 所有功能都在 MiniGame Area 內完成

## 注意事項
- 所有修改都必須在 Areas/MiniGame 目錄內進行
- 不能修改其他區域的文件
- 使用 Area 內部的數據庫上下文和服務

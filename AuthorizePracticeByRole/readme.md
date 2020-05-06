## 不使用 Ef Core 的原因：

因為 Ef Core 3.1 的語法所參考的 SqlParameter 似乎與 .Net Framework 所參考的 SqlParameter 不同

所以先改成 Dapper

## Db 專案

可直接 publish 至 SQL Server

SeedData.sql 包含初始資料

```
Account:admin
Password:1234
```

Web.config 指定的 DataBase 名稱為 `AuthorizePracticeByRole`

## Web.config
使用 Authentication `Forms` 方式 
```xml
<authentication mode="Forms">
    <forms defaultUrl="~/Home/Index/" loginUrl="~/Auth/Login/" timeout="30" />
</authentication>
```

## AuthController

Authentication 實作

實作登入/登出功能

登入後，把使用者資訊放入 UserData 中

## BaseController

從 System.Web.HttpContext.Current.User.Identity.Ticket.UserData 取出使用者資料

從使用者資料來判斷使用者是否登入

### 擴充 ControllerActionInvoker

設計構想

- 以最單純的角度來設計
- Action 可以透過 CustomAuthorizeAttribute() 給定允許進入的 Role


Authorization 實作

判斷 Action 上面的 CustomAuthorizeAttribute() 是否符合該使用者的資格

## MemberController

用來驗証 Authorization 功能是否正確

TODO:

- 加上 Authorization 編輯功能
- 加上新增功能
- 加上編輯功能
- 加上刪除功能

# FileUpload-CoreMVC  WithOut Dependency Injection
Asp.Net Core Mvc C# Entity Framework Razor WithOut Dependency Injection

fILE Path => wwwroot\Content\Images

View => Folder Emp => AddEmp.cshtml , EditEmp.cshtml And Index.cshtml

Controllers => EmpControllers.cs

Models => Emp.cs And EmpView.cs    Both For Same one is model and one it not entity

Data => ApplicationDbContext.cs


-- Sugar Start  --
=> On View Open Attachmnet in Tab and also download very short Trick using  target="_blank" and download Attribute inside the anchor tag

  <ul>
      @if (!string.IsNullOrEmpty(Model.AttachmentPaths))
      {
          foreach (var filePath in Model.AttachmentPaths.Split(','))
          {
              <li>
                  <a href="~/Attachments/@filePath" target="_blank">@filePath</a>
                  <a href="~/Attachments/@filePath" download>Download</a>
              </li>
          }
      }
      else
      {
          <li>No attachments found.</li>
      }
  </ul>


Controller Code:      
public IActionResult ViewNotice(int id)
     {
         var notice = _noticeService.GetNoticeById(id);
         if (notice == null || notice.ExpiryDate < DateTime.Now)
             return NotFound();

         return View(notice);
     }

Column Name: AttachmentPaths
-- Sugar End  --

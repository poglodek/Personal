namespace Notification.Templates.User.ActivateUser;

public class ActivateUserTemplate : ITemplate<ActivateUserTemplateData>
{
    private const string Html = @"
<html>
    <body>
        <h1>Welcome in Personal App</h1><br /><br />
        <h2><p> We are so greatful that you have decided to join us. </p></h2><br /><br />
        <p> To activate your account click <a href='{0}'>here</a></p>
        <p> If you have any questions, please contact us at <a href='mailto:kontakt@personal.app'>
        <br /><br /><br />
        <p>Best regards</p><br />
        <p>Personal App Team</p>
    </body>
</html
";
    
    
    public string GetReadyTemplate(ActivateUserTemplateData data)
    {
        return string.Format(Html, data.Url);
    }
}
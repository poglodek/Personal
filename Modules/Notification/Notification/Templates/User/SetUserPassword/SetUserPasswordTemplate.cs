namespace Notification.Templates.User.SetUserPassword;

public class SetUserPasswordTemplate : ITemplate<SetUserPasswordTemplateData>
{
    private const string Html = @"
<html>
    <body>
        <h1>Personal App Here</h1><br /><br />
        <h2><p> So you decied to set new password to your account </p></h2><br /><br />
        <p> To to set new password click <a href='{0}'>here</a></p>
        <p> If you have any questions, please contact us at <a href='mailto:kontakt@personal.app'>
        <br /><br /><br />
        <p>Best regards</p><br />
        <p>Personal App Team</p>
    </body>
</html
";

    
    public string GetReadyTemplate(SetUserPasswordTemplateData data)
    {
        return string.Format(Html, data.Url);
    }
}
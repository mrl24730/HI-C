<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncryptSQL.aspx.cs" Inherits="WebPortal.findStuffs.EncryptSQL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Encrypt</title>
</head>
<body>
    <form id="submitForm" action="EncryptSQL.aspx" method="post">
    <div>
        s: <textarea name="sql" id="sql" style="width:500px;height:300px;" /><%=sql%></textarea>
        <br />
        r: <input type="text" name="r" id="r" value="<%=r %>"/>
        <br />

        result: <textarea name="result" id="result" /></textarea>
        <br />

        <input type="submit" value="submit"/>
    </div>
    </form>

    <p>
    
    <%=en_sql%>
    </p>

    <p>
    <br />
    <%=result%>
    
    </p>

</body>
</html>

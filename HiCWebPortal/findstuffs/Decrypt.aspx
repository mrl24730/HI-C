o<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Decrypt.aspx.cs" Inherits="WebPortal.findstuffs.Decrypt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Decrypt / Encrypt</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Encrypted IP / Email etc: <input type="text" value="" id="encrypt" name="encrypt" /></p>

        <p>Decrypted: <%=decryptedText %></p>

        <p><input type="submit" value="Decrypt" id="send"  /></p>
    </div>
    </form>
    <br />
    <br />
    <form id="form2" method="post">
    <div>
        <p>Email / Mobile etc: <input type="text" value="<%=plainText %>" id="plain" name="plain" /></p>

        <p>Encrypted:</p>
        <p><%=encryptedText %></p>

        <p><input type="submit" value="Encrypt" id="send2"  /></p>
    </div>

    </form>
</body>
</html>

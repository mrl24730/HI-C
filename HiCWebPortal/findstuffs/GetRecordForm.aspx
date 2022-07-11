<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetRecordForm.aspx.cs" Inherits="WebPortal.findstuffs.GetRecordForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form</title>
    <script type="text/javascript">
        function chgServer(v) {
            var frm = document.getElementById("submitForm");
            frm.action = v;
        }

        function localServerReqAddress() {
            var opt = document.getElementById("localserver");
            opt.value = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/FindStuffs/getRecords";
            chgServer(opt.value);
           
        }
    </script>
</head>
<body onload="localServerReqAddress();" style="font-family:arial;">
    <form id="submitForm" target="_blank" method="post" action="">
    <div>
        h: <input type="text" name="h" id="h" value="<%=hash %>"/>
        <br />
        r: <input type="text" name="r" id="r" value="<%=r %>"/>
        <br />
        o: <input type="text" name="o" id="o" value="r"/> [ r: reader, n: nonquery, s: scalar ]
        <br />
        s: <textarea name="s" id="s" style="width:400px; height:250px;" ></textarea>
        <br />
        t: <input type="text" name="t" id="t" value="<%=currentTS %>" />
        <br />
        <select name="server" id="server" onchange="chgServer(this.value);" >
            <option id="localserver" value="">Local</option>
            <option value="http://dev014827.hi-c.com.hk/FindStuffs/getRecords">Testing</option>
            <option value="https://www.hi-c.com.hk/FindStuffs/getRecords">Production</option>
            
        </select>
        <br />
        <input type="submit" value="submit"/>
    </div>
    </form>
</body>
</html>

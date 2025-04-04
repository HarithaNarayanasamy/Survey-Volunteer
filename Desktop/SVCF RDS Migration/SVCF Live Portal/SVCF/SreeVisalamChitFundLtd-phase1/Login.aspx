<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Login" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SVCF - Login Page</title>
    <!-- Foundation framework -->
    <link rel="stylesheet" href="pertho_admin_v1.3/foundation/stylesheets/foundation.css" />
    <link rel="stylesheet" href="pertho_admin_v1.3/css/style.css" />
    <!-- Favicons and the like (avoid using transparent .png) -->
    <link rel="apple-touch-icon-precomposed" href="pertho_admin_v1.3/icon.png" />
    <style type="text/css">
        .loginheader
        {
            background: #def;
            border-radius: 5px 5px 0 0;
            height: 90px;
            text-align: center;
        }
        .inset-text
        {
            /* Shadows are visible under slightly transparent text color */
            color: rgba(10,60,150, 0.8);
            text-shadow: 1px 4px 6px #def, 0 0 0 #000, 1px 4px 6px #def;
        }
    </style>
</head>
<body class="ptrn_a grdnt_a">
    <div class="container">
        <div class="row">
            <div class="eight columns centered">
            </div>
        </div>
        <div class="row">
            <div class="eight columns centered">
                <div class="login_box">
                    <div class="lb_content">
                        <div class="loginheader">
                            <div style="float: left; width: 100px;">
                                <img src="Styles/Image/logo_New.png" alt="" /></div>
                            <div style="margin-left: 100px; width: 100%; height: 90px; vertical-align: middle;
                                display: table-cell; text-align: center;">
                                <p style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 25px;"
                                    class="inset-text">
                                    Sree Visalam Chit Fund Ltd.,</p>
                            </div>
                        </div>
                        <div class="cf">
                            <h2 class="lb_ribbon lb_blue">
                                <span>Login to your account</span><span style="display: none">New password</span></h2>
                            <a href="#" class="right small sl_link"><span>Forgot your password?</span> <span
                                style="display: none">Back to login form</span> </a>
                        </div>
                        <div class="row m_cont">
                            <div class="eight columns centered">
                                <div class="l_pane">
                                    <form runat="server" class="nice" id="l_form">
                                    <div class="sepH_c">
                                        <div>
                                            <label for="ddlBranch">
                                                Branch Name</label>
                                            <asp:DropDownList class="oversize expand" ID="ddlBranch" Width="220px" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="elVal">
                                            <label for="txtUser">
                                                User Name</label>
                                            <asp:TextBox runat="server" ID="txtUser" name="username" class="oversize expand input-text" />
                                        </div>
                                        <div class="elVal">
                                            <label for="txtPassword">
                                                Password</label>
                                            <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" name="password"
                                                class="oversize expand input-text" />
                                        </div>
                                    </div>
                                    <div class="cf">
                                        <label for="remember" class="left">
                                            <%--<input type="checkbox" id="remember">
                                            Remember me--%></label>
                                        <asp:Button Text="Login" runat="server" OnClick="btnlogin_Click" class="button small radius right black"
                                            value="Login" />
                                    </div>
                                    </form>
                                </div>
                                <div class="l_pane" style="display: none">
                                    <form class="nice" id="rp_form">
                                    <div class="sepH_c">
                                        <p class="sepH_b">
                                            Please enter your email address. You will receive Your Credential Details via email.</p>
                                        <div class="elVal">
                                            <label for="txtEmail">
                                                E-mail:</label>
                                            <input type="text" id="txtEmail" name="upname" class="oversize expand input-text" />
                                        </div>
                                    </div>
                                    <div class="cf">
                                        <input type="submit" class="button small radius right black" value="Get new password" />
                                    </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="pertho_admin_v1.3/js/jquery.min.js"></script>
    <script src="pertho_admin_v1.3/js/s_scripts.js"></script>
    <script src="pertho_admin_v1.3/lib/validate/jquery.validate.min.js"></script>
    <script>
        $(document).ready(function () {
            $(".sl_link").click(function (event) {
                $('.l_pane').slideToggle('normal').toggleClass('dn');
                $('.sl_link,.lb_ribbon').children('span').toggle();
                event.preventDefault();
            });

            $("#l_form").validate({
                highlight: function (element) {
                    $(element).closest('.elVal').addClass("form-field error");
                },
                unhighlight: function (element) {
                    $(element).closest('.elVal').removeClass("form-field error");
                },
                rules: {
                    txtUser: "required",
                    txtPassword: "required"
                },
                messages: {
                    txtUser: "Please enter your username ",
                    txtPassword: "Please enter a password "
                },
                errorPlacement: function (error, element) {
                    error.appendTo(element.closest(".elVal"));
                }
            });

            $("#rp_form").validate({
                highlight: function (element) {
                    $(element).closest('.elVal').addClass("form-field error");
                },
                unhighlight: function (element) {
                    $(element).closest('.elVal').removeClass("form-field error");
                },
                rules: {
                    upname: {
                        required: true,
                        email: true
                    }
                },
                messages: {
                    upname: "Please enter a valid email address"
                },
                errorPlacement: function (error, element) {
                    error.appendTo(element.closest(".elVal"));
                }
            });
        });
    </script>
</body>
</html>

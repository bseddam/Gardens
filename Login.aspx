<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Default1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Giriş səhifəsi</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <!-- base:css -->
    <link href="vendors/mdi/css/materialdesignicons.min.css" rel="stylesheet" />
    <link href="vendors/css/vendor.bundle.base.css" rel="stylesheet" />
    <link rel="stylesheet" href="vendors/css/vendor.bundle.base.css" />
    <!-- endinject -->
    <!-- plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link href="css/horizontal-layout-light/style.css" rel="stylesheet" />

    <!-- endinject -->
   


</head>
<body>
    <div class="container-scroller">
        <div class="container-fluid page-body-wrapper full-page-wrapper">
            <div class="content-wrapper d-flex align-items-stretch auth auth-img-bg">
                <div class="row flex-grow">
                    <div class="col-lg-6 d-flex align-items-center justify-content-center">
                        <div class="auth-form-transparent text-left p-3">
                        <%--    <div class="brand-logo">
                               

                            </div>--%>

                            <h3>BAĞLAR</h3>
                          
                            <form class="pt-3" runat="server">
                                <div class="form-group">
                                    <label for="exampleInputEmail">İstifadəçi adı</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="mdi mdi-account-outline text-primary"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtusername" placeholder="İstifadəçi adı" class="form-control form-control-lg border-left-0" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword">Şifrə</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="mdi mdi-lock-outline text-primary"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtpassword" TextMode="Password" placeholder="Şifrə" class="form-control form-control-lg border-left-0" runat="server"></asp:TextBox>


                                    </div>
                                </div>
                                <div class="my-2 d-flex justify-content-between align-items-center">
                                    <div class="form-check">
                                         <asp:Label Text="" ForeColor="Red" ID="lblPopError" runat="server" />
                             
                                        <label class="form-check-label text-muted">
                                            <input type="checkbox" class="form-check-input" runat="server">
                                            Yadda saxla
                   
                                        </label>
                                    </div>
                                   <%-- <a href="#" class="auth-link text-black">Şifrəni unutdum?</a>--%>
                                </div>
                                <div class="my-3">
                                    <asp:Button ID="btntesdiq" runat="server" Text="Daxil olun" class="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" OnClick="btntesdiq_Click" />


                                </div>
                                <%--                <div class="mb-2 d-flex">
                  <button type="button" class="btn btn-facebook auth-form-btn flex-grow mr-1">
                    <i class="mdi mdi-facebook mr-2"></i>Facebook
                  </button>
                  <button type="button" class="btn btn-google auth-form-btn flex-grow ml-1">
                    <i class="mdi mdi-google mr-2"></i>Google
                  </button>
                </div>--%>
                                <%--                <div class="text-center mt-4 font-weight-light">
                  Qeydiyyatdan keçməmisiniz? <a href="register-2.html" class="text-primary">Qeydiyyat</a>
                </div>--%>
                            </form>
                        </div>
                    </div>
                    <div class="col-lg-6 login-half-bg d-flex flex-row">
                        <p class="text-white font-weight-medium text-center flex-grow align-self-end">Bütün hüquqlar qorunur © 2022</p>
                    </div>
                </div>
            </div>
            <!-- content-wrapper ends -->
        </div>
        <!-- page-body-wrapper ends -->
    </div>
    <!-- container-scroller -->
    <!-- base:js -->
    <script src="vendors/js/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page-->
    <!-- End plugin js for this page-->
    <!-- inject:js -->
    <script src="js/off-canvas.js"></script>
    <script src="js/hoverable-collapse.js"></script>
    <script src="js/template.js"></script>
    <script src="js/settings.js"></script>
    <script src="js/todolist.js"></script>
    <!-- endinject -->
    <!-- plugin js for this page -->
    <!-- End plugin js for this page -->
    <!-- Custom js for this page-->
    <!-- End custom js for this page-->

</body>
</html>

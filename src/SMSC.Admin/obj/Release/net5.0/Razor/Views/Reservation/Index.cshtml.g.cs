#pragma checksum "C:\Users\lenovo\Downloads\Interns Working Template  NEW_Updated 2\Interns Working Template  NEW_Updated\Interns Working Template\src\SMSC.Admin\Views\Reservation\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa70"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reservation_Index), @"mvc.1.0.view", @"/Views/Reservation/Index.cshtml")]
namespace AspNetCore
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\lenovo\Downloads\Interns Working Template  NEW_Updated 2\Interns Working Template  NEW_Updated\Interns Working Template\src\SMSC.Admin\Views\_ViewImports.cshtml"
using SMSC.Admin

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa70", @"/Views/Reservation/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"e7b9d94e5e0197ea2395279f7be6de808db9d0dd4b132c0bef9f66716f50d128", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Reservation_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("formAddReservation"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("formUpdateReservation"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "Waiting", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "Accepted", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "Cancelled", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("formUpdateReservationStatus"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/script/reservation.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<title>Reservation Management</title>


<h4 class=""page-title mt-4 text-center"">Reservation Details</h4>
<div class=""row"">
    <div class=""col-md-12"">
        <div id=""ReservationGrid"" class=""ag-theme-quartz"" style=""height: 800px; width: 100%;""></div>
    </div>
</div>
<!-- Modal -->
<div class=""modal fade"" id=""addReservationModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""addReservationModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""addReservationModalLabel"">Add Reservation</h5>
                <button type=""button"" class=""close"" onclick=""$('#addReservationModal').modal('hide')"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa707044", async() => {
                WriteLiteral(@"
                    <div class=""row"">
                        <div class=""col-4 mb-3"">
                            <label for=""FirstName"" class=""form-label"">First Name</label>
                            <input type=""text"" class=""form-control"" id=""FirstName"" placeholder=""Enter First Name"" name=""FirstName"" required data-parsley-required-message=""First Name is required"">
                        </div>
                        <div class=""col-4 mb-3"">
                            <label for=""LastName"" class=""form-label"">Last Name</label>
                            <input type=""text"" class=""form-control"" id=""LastName"" placeholder=""Enter Last Name"" name=""LastName"" required data-parsley-required-message=""Last Name is required"">
                        </div>

                        <div class=""col-4 mb-3"">
                            <label for=""Email"" class=""form-label"">Email</label>
                            <input type=""email"" class=""form-control"" id=""Email"" name=""Email"" placeholder=""Enter Email""");
                WriteLiteral(@" required data-parsley-required-message=""Email is required"" data-parsley-type-message=""Please enter a valid email"">
                        </div>
                    </div>

                    <div class=""row"">
                        <div class=""col-4 mb-3"">
                            <label for=""Address"" class=""form-label"">Address</label>
                            <input type=""text"" class=""form-control"" id=""Address"" placeholder=""Enter Address Name"" name=""Address"" required data-parsley-required-message=""Address is required"">
                        </div>

                        <div class=""col-4 mb-3"">
                            <label for=""PhoneNo"" class=""form-label"">Phone Number</label>
                            <input type=""tel"" class=""form-control"" id=""PhoneNo"" placeholder=""Enter Phone Number"" name=""PhoneNo"" required pattern=""\d*"" data-parsley-required-message=""Phone number is required"" data-parsley-pattern-message=""Please enter a valid phone number containing only digits"">

     ");
                WriteLiteral(@"                   </div>

                    </div>

                    <div class=""row mb-3"">
                        <div class=""col-6 mb-3"">
                            <label for=""ReservationDateTime"" class=""form-label"">Reservation Date and Time</label>
                            <div class=""input-group date"" id=""ReservationDateTime"" data-target-input=""nearest"">
                                <input type=""text"" class=""form-control datetimepicker-input"" name=""ReservationDateTime"" data-target=""#ReservationDateTime"" required data-parsley-required-message=""Add Reservation Date and Time"" />
                                <div class=""input-group-append"" data-target=""#ReservationDateTime"" data-toggle=""datetimepicker"">
                                    <div class=""input-group-text""><i class=""fa fa-calendar""></i></div>
                                </div>
                            </div>
                        </div>

                        <div class=""col-6 mb-3"">
                   ");
                WriteLiteral(@"         <label for=""AdvanceAmount"" class=""form-label"">Advance Amount</label>
                            <input type=""number"" class=""form-control"" id=""AdvanceAmount"" name=""AdvanceAmount"" placeholder=""Enter Advance Amount"" step=""0.01"" min=""0"" required data-parsley-required-message=""Advance amount is required"" data-parsley-type-message=""Please enter a valid amount"">
                        </div>
                    </div>
                  

               

                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-light"" data-bs-dismiss=""modal"">Close</button>
                        <button type=""submit"" class=""btn btn-primary"">Submit</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("data-parsley-validate", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>



<div class=""modal fade"" id=""updateReservationModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""updateReservationModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""updateReservationModalLabel"">Update Reservation</h5>
                <button type=""button"" class=""close"" onclick=""$('#updateReservationModal').modal('hide')"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa7013575", async() => {
                WriteLiteral(@"
                    <input type=""text"" id=""GuestId"" name=""GuestId"" hidden />
                    <div class=""row"">
                        <div class=""col-4 mb-3"">
                            <label for=""updatedFirstName"" class=""form-label"">First Name</label>
                            <input type=""text"" class=""form-control"" id=""updatedFirstName"" placeholder=""Enter First Name"" name=""FirstName"" required data-parsley-required-message=""Name is required"" readonly>
                        </div>
                        <div class=""col-4 mb-3"">
                            <label for=""updatedLastName"" class=""form-label"">Last Name</label>
                            <input type=""text"" class=""form-control"" id=""updatedLastName"" placeholder=""Enter Last Name"" name=""LastName"" required data-parsley-required-message=""Name is required"" readonly>
                        </div>

                        <div class=""col-4 mb-3"">
                            <label for=""updatedEmail"" class=""form-label"">Email</label>");
                WriteLiteral(@"
                            <input type=""email"" class=""form-control"" id=""updatedEmail"" name=""Email"" placeholder=""Enter Email"" required data-parsley-required-message=""Email is required"" data-parsley-type-message=""Please enter a valid email"" readonly>
                        </div>
                    </div>

                    <div class=""row"">
                        <div class=""col-4 mb-3"">
                            <label for=""updatedAddress"" class=""form-label"">Address</label>
                            <input type=""text"" class=""form-control"" id=""updatedAddress"" placeholder=""Enter Address Name"" name=""Address"" required data-parsley-required-message=""Address is required"" readonly>
                        </div>

                        <div class=""col-4 mb-3"">
                            <label for=""updatedPhoneNo"" class=""form-label"">Phone Number</label>
                            <input type=""tel"" class=""form-control"" id=""updatedPhoneNo"" placeholder=""Enter Phone Number"" name=""PhoneNo"" requ");
                WriteLiteral(@"ired pattern=""\d*"" data-parsley-required-message=""Phone number is required"" data-parsley-pattern-message=""Please enter a valid phone number containing only digits"" readonly>

                        </div>


                    </div>

                    <div class=""row mb-3"">
                        <div class=""col-6 mb-3"">
                            <label for=""updatedReservationDateTime"" class=""form-label"">Reservation Date and Time</label>
                            <div class=""input-group date"" id=""updatedReservationDateTime"" data-target-input=""nearest"">
                                <input type=""text"" class=""form-control datetimepicker-input"" name=""ReservationDateTime"" data-target=""#updatedReservationDateTime"" />
                                <div class=""input-group-append"" data-target=""#updatedReservationDateTime"" data-toggle=""datetimepicker"">
                                    <div class=""input-group-text""><i class=""fa fa-calendar""></i></div>
                                </div>");
                WriteLiteral(@"
                            </div>
                        </div>

                        <div class=""col-6 mb-3"">
                            <label for=""updatedAdvanceAmount"" class=""form-label"">Advance Amount</label>
                            <input type=""number"" class=""form-control"" id=""updatedAdvanceAmount"" name=""AdvanceAmount"" placeholder=""Enter Advance Amount"" step=""0.01"" min=""0"" required data-parsley-required-message=""Advance amount is required"" data-parsley-type-message=""Please enter a valid amount"">
                        </div>
                    </div>

           

                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-light"" data-bs-dismiss=""modal"">Close</button>
                        <button type=""submit"" class=""btn btn-primary"">Submit</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("data-parsley-validate", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>





<div class=""modal fade"" id=""updateReservationStatusModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""updateReservationStatusModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-xl"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""updateReservationStatusModalLabel"">Update Status</h5>
                <button type=""button"" class=""close"" onclick=""$('#updateReservationStatusModal').modal('hide')"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>


            <div class=""modal-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa7020269", async() => {
                WriteLiteral(@"
                    <input type=""text"" id=""updatedGuestId"" name=""GuestId"" hidden />

                    <div class=""col-12 mb-3"">
                        <label for=""FloorNo"" class=""form-label"">Change Status</label>
                        <select class=""js-example-basic-single js-states form-control"" id=""ReservationStatus"" name=""ReservationStatus"" required data-parsley-required-message=""Reservation Status is required"">
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa7021029", async() => {
                    WriteLiteral(" Waiting");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa7022307", async() => {
                    WriteLiteral(" Accepted");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa7023586", async() => {
                    WriteLiteral(" Cancelled");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"

                        </select>
                    </div>





                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-light"" data-bs-dismiss=""modal"">Close</button>
                        <button type=""submit"" class=""btn btn-primary"">Submit</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("data-parsley-validate", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n");
            DefineSection("scriptsAfter", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "233b94de0ade8211f5645a0023dfe95abded2de714b4e1d624667252299faa7026778", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n ");
            }
            );
            WriteLiteral("\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

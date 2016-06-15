using Eiap.Framework.Base.AssemblyService.SXW;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eiap.Framework.Common.Logger.LocalFile.Test
{
    public partial class Form1 : Form
    {
        ILogger logger = null;
        public Form1()
        {
            InitializeComponent();
            AssemblyManager.Instance.RegisterAssembly(@"C:\MyWork\EiapV3.0\Eiap.Framework\Eiap.Framework.Common.Logger.LocalFile.Test\bin\Debug")
                .Register(DependencyManager.Instance.Register);
            logger = (ILogger)DependencyManager.Instance.Resolver(typeof(ILogger));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(WriteLog));
            thread.Start();
        }

        private void WriteLog()
        {
            for (int i = 0; i < 1000; i++)
            {
                logger.Error(@"Abp.WebApi.Controllers.Filters.AbpExceptionFilterAttribute
Abp.UserFriendlyException: 对不起,在处理您的请求期间,产生了一个服务器内部错误!
   在 YMC.ECService.Core.AbpWebApiClient.<RequestAsync>d__e`1.MoveNext() 位置 c:\work\YaoMaiChe\源代码\YMC_Platform\YMC_ECService\YMC.ECService.Modules\YMC.ECService.Core\Application\Client\AbpWebApiClient.cs:行号 186
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   在 YMC.ECService.Core.AbpWebApiClient.<PostAsync>d__9`1.MoveNext() 位置 c:\work\YaoMaiChe\源代码\YMC_Platform\YMC_ECService\YMC.ECService.Modules\YMC.ECService.Core\Application\Client\AbpWebApiClient.cs:行号 62
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   在 YMC.ECService.Core.AbpWebApiClient.<PostAsync>d__3.MoveNext() 位置 c:\work\YaoMaiChe\源代码\YMC_Platform\YMC_ECService\YMC.ECService.Modules\YMC.ECService.Core\Application\Client\AbpWebApiClient.cs:行号 52
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   在 Nito.AsyncEx.Synchronous.TaskExtensions.WaitAndUnwrapException(Task task)
   在 Nito.AsyncEx.AsyncContext.<>c__DisplayClass3.<Run>b__1(Task t)
   在 System.Threading.Tasks.ContinuationTaskFromTask.InnerInvoke()
   在 System.Threading.Tasks.Task.Execute()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   在 Nito.AsyncEx.Synchronous.TaskExtensions.WaitAndUnwrapException(Task task)
   在 Nito.AsyncEx.AsyncContext.Run(Func`1 action)
   在 Abp.Threading.AsyncHelper.RunSync(Func`1 action)
   在 YMC.ECService.Products.Domain.Orders.ActivityOrderPaySuccessHandler.ActivityOrderPaySuccessRefundEnrollingPriceHandler.PaySuccessRefundEnrollingPrice(PanicbuyingOrderPaySuccessContext context) 位置 c:\work\YaoMaiChe\源代码\YMC_Platform\YMC_ECService\YMC.ECService.Modules\03_Product\YMC.ECService.Product.Domain\Orders\ActivityOrderPaySuccessHandler\ActivityOrderPaySuccessRefundEnrollingPriceHandler.cs:行号 42
   在 YMC.ECService.Products.Domain.Orders.ActivityOrderPaySuccessHandler.ActivityOrderPaySuccessRefundEnrollingPriceHandler.Process(BusinessContextBase orderContext) 位置 c:\work\YaoMaiChe\源代码\YMC_Platform\YMC_ECService\YMC.ECService.Modules\03_Product\YMC.ECService.Product.Domain\Orders\ActivityOrderPaySuccessHandler\ActivityOrderPaySuccessRefundEnrollingPriceHandler.cs:行号 35
   在 YMC.ECService.Core.BusinessManager.BusinessContextManagerBase.<>c__DisplayClass1`1.<BusinessProcess>b__0(Type m) 位置 c:\work\YaoMaiChe\源代码\YMC_Platform\YMC_ECService\YMC.ECService.Modules\YMC.ECService.Core\BusinessManager\BusinessContextManagerBase.cs:行号 32
   在 System.Collections.Generic.List`1.ForEach(Action`1 action)
   在 YMC.ECService.Core.BusinessManager.BusinessContextManagerBase.BusinessProcess[TContextManagerImpType](BusinessContextBase businessContext) 位置 c:\work\YaoMaiChe\源代码\YMC_Platform\YMC_ECService\YMC.ECService.Modules\YMC.ECService.Core\BusinessManager\BusinessContextManagerBase.cs:行号 26
   在 YMC.ECService.Products.Application.OrderAppService.<ChangeOrderStatus>d__50.MoveNext() 位置 c:\work\YaoMaiChe\源代码\YMC_Platform\YMC_ECService\YMC.ECService.Modules\03_Product\YMC.ECService.Product.Application\Orders\OrderAppService.cs:行号 387
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   在 Abp.Threading.InternalAsyncHelper.<AwaitTaskWithPostActionAndFinally>d__4.MoveNext()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   在 Abp.Threading.InternalAsyncHelper.<AwaitTaskWithFinally>d__0.MoveNext()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   在 System.Threading.Tasks.TaskHelpersExtensions.<CastToObject>d__0.MoveNext()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   在 Abp.Extensions.ExceptionExtensions.ReThrow(Exception exception)
   在 Abp.WebApi.Controllers.Dynamic.Selectors.DynamicHttpActionDescriptor.<ExecuteAsync>b__0(Task`1 task)
   在 System.Threading.Tasks.ContinuationResultTaskFromResultTask`2.InnerInvoke()
   在 System.Threading.Tasks.Task.Execute()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   在 System.Web.Http.Controllers.ApiControllerActionInvoker.<InvokeActionAsyncCore>d__0.MoveNext()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   在 System.Web.Http.Filters.ActionFilterAttribute.<CallOnActionExecutedAsync>d__5.MoveNext()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   在 System.Web.Http.Filters.ActionFilterAttribute.<CallOnActionExecutedAsync>d__5.MoveNext()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   在 System.Web.Http.Filters.ActionFilterAttribute.<ExecuteActionFilterAsyncCore>d__0.MoveNext()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   在 System.Web.Http.Controllers.ActionFilterResult.<ExecuteAsync>d__2.MoveNext()
--- 引发异常的上一位置中堆栈跟踪的末尾 ---
   在 System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   在 System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   在 System.Web.Http.Controllers.ExceptionFilterResult.<ExecuteAsync>d__0.MoveNext()", "", 0, "LocalFileTest");
            }
        }
    }
}

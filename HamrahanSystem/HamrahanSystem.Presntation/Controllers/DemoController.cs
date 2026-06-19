using HamrahanSystem.Presntation.Models.Demo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HamrahanSystem.Presntation.Controllers;

[AllowAnonymous]
public class DemoController : Controller
{
    public IActionResult Index()
    {
        return View(BuildLandingModel());
    }

    public IActionResult Workspace()
    {
        return View(BuildWorkspaceModel());
    }

    private static PublicDemoLandingViewModel BuildLandingModel()
    {
        return new PublicDemoLandingViewModel
        {
            Title = "AdelLens Public Demo",
            Subtitle = "نسخه عمومی و قابل انتشار از پورتال سفارش و مدیریت عدسی",
            Summary = "این نسخه برای نمایش تجربه کاربری، ماژول‌های اصلی و منطق کلی سیستم آماده شده و به هیچ داده سازمانی یا زیرساخت داخلی متصل نیست.",
            Highlight = "Public Demo",
            Stats =
            [
                new DemoStat("14", "ماژول نمایشی", "از سفارش تا مدیریت کاتالوگ"),
                new DemoStat("3", "سناریوی اصلی", "سفارش، پیگیری، و مدیریت محصول"),
                new DemoStat("100%", "داده نمایشی", "بدون وابستگی به دیتابیس عملیاتی")
            ],
            Features =
            [
                new DemoFeature("ثبت سفارش عدسی", "فرم‌های سفارش، اطلاعات نسخه، و انتخاب محصول در یک جریان یکپارچه."),
                new DemoFeature("کاتالوگ محصولات", "نمایش برندها، لایه‌ها، جنس‌ها و شاخص‌های عدسی به‌صورت ساختاریافته."),
                new DemoFeature("داشبورد پیگیری", "مرور وضعیت سفارش‌ها، گلوگاه‌های فرآیند و وضعیت انجام کار."),
                new DemoFeature("آماده برای GitHub", "نسخه‌ای ایزوله برای انتشار عمومی و استفاده در رزومه یا نمونه‌کار.")
            ],
            Stories =
            [
                new DemoStory("Retail Order Desk", "ثبت سریع نسخه مشتری و تبدیل آن به سفارش قابل پیگیری."),
                new DemoStory("Lab Coordination", "ردیابی وضعیت سفارش‌ها از ثبت تا آماده‌سازی و تحویل."),
                new DemoStory("Product Governance", "نمایش مدیریت ساختار برند، coating، material و design type.")
            ]
        };
    }

    private static PublicDemoWorkspaceViewModel BuildWorkspaceModel()
    {
        return new PublicDemoWorkspaceViewModel
        {
            Title = "Demo Workspace",
            Overview = "نمایی نمایشی از داشبورد عملیاتی AdelLens با داده‌های ساختگی اما نزدیک به سناریوی واقعی.",
            SummaryCards =
            [
                new DemoSummaryCard("سفارش‌های فعال", "28", "7 مورد در انتظار تایید نهایی", "text-success"),
                new DemoSummaryCard("میانگین زمان آماده‌سازی", "42h", "3 ساعت بهتر از هفته قبل", "text-primary"),
                new DemoSummaryCard("محصولات سفارشی", "12", "به‌همراه coating و material اختصاصی", "text-warning"),
                new DemoSummaryCard("رضایت مشتریان", "96%", "بر پایه داده نمایشی دمو", "text-info")
            ],
            Orders =
            [
                new DemoOrderRow("AL-24051", "کلینیک پارس", "Progressive 1.67 Blue Cut", "در حال تولید", "امروز"),
                new DemoOrderRow("AL-24052", "بینایی سپهر", "Stock Lens UV 400", "آماده ارسال", "امروز"),
                new DemoOrderRow("AL-24048", "مرکز اپتیک دیدار", "Office Lens Premium", "در انتظار تایید", "دیروز"),
                new DemoOrderRow("AL-24041", "عینک ایران", "Photochromic 1.56", "تحویل شده", "2 روز پیش")
            ],
            Catalog =
            [
                new DemoCatalogItem("Adel Prime", "Single Vision", "1.56 / 1.61 / 1.67", "Blue Cut, HMC, UV"),
                new DemoCatalogItem("Urban Office", "Office Lens", "1.56 / 1.60", "Soft Corridor, Anti-Fatigue"),
                new DemoCatalogItem("Drive Pro", "Progressive", "1.61 / 1.67", "Night Drive, Super AR")
            ],
            Workflow =
            [
                new DemoWorkflowStep("ثبت سفارش", "اطلاعات نسخه، فریم و نیاز مشتری ثبت می‌شود."),
                new DemoWorkflowStep("انتخاب محصول", "لنز، coating، material و گزینه‌های تکمیلی انتخاب می‌شوند."),
                new DemoWorkflowStep("پیگیری عملیات", "وضعیت سفارش در کارتابل و داشبورد قابل ردیابی است."),
                new DemoWorkflowStep("تحویل و بایگانی", "سفارش تکمیل و سابقه آن برای مراجعات بعدی حفظ می‌شود.")
            ]
        };
    }
}

namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Common.BankStatementAnalyzerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Common.BankStatementAnalyzerContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                System.Diagnostics.Debugger.Launch();
            }

            base.Seed(context);

            if (context.Action.ToList().Count() == 0)
            {
                Models.Action homeIndex = new Models.Action { ID = 1, Name = "ACTION.Home.Index", Feature = "Home", DisplayName = "ACTION.Home.Index", CreatedOn = DateTime.Now };

                Models.Action roleIndex = new Models.Action { ID = 2, Name = "ACTION.RoleMaster.Index", Feature = "Role", DisplayName = "ACTION.RoleMaster.Index", CreatedOn = DateTime.Now };
                Models.Action roleCreate = new Models.Action { ID = 3, Name = "ACTION.RoleMaster.Create", Feature = "Role", DisplayName = "ACTION.RoleMaster.Create", CreatedOn = DateTime.Now };
                Models.Action roleEdit = new Models.Action { ID = 4, Name = "ACTION.RoleMaster.Edit", Feature = "Role", DisplayName = "ACTION.RoleMaster.Edit", CreatedOn = DateTime.Now };
                Models.Action roleDelete = new Models.Action { ID = 5, Name = "ACTION.RoleMaster.Delete", Feature = "Role", DisplayName = "ACTION.RoleMaster.Delete", CreatedOn = DateTime.Now };

                Models.Action accountLogOff = new Models.Action { ID = 6, Name = "ACTION.Account.LogOff", Feature = "Account", DisplayName = "ACTION.Account.LogOff", CreatedOn = DateTime.Now };

                Models.Action userIndex = new Models.Action { ID = 7, Name = "ACTION.User.Index", Feature = "User", DisplayName = "ACTION.User.Index", CreatedOn = DateTime.Now };
                Models.Action userCreate = new Models.Action { ID = 8, Name = "ACTION.User.Create", Feature = "User", DisplayName = "ACTION.User.Create", CreatedOn = DateTime.Now };
                Models.Action userEdit = new Models.Action { ID = 9, Name = "ACTION.User.Edit", Feature = "User", DisplayName = "ACTION.User.Edit", CreatedOn = DateTime.Now };
                Models.Action userDelete = new Models.Action { ID = 10, Name = "ACTION.User.Delete", Feature = "User", DisplayName = "ACTION.User.Delete", CreatedOn = DateTime.Now };

                Models.Action menuIndex = new Models.Action { ID = 11, Name = "ACTION.Menu.Index", Feature = "Menu", DisplayName = "ACTION.Menu.Index", CreatedOn = DateTime.Now };
                Models.Action menuCreate = new Models.Action { ID = 12, Name = "ACTION.Menu.Create", Feature = "Menu", DisplayName = "ACTION.Menu.Create", CreatedOn = DateTime.Now };
                Models.Action menuEdit = new Models.Action { ID = 13, Name = "ACTION.Menu.Edit", Feature = "Menu", DisplayName = "ACTION.Menu.Edit", CreatedOn = DateTime.Now };

                Models.Action actionIndex = new Models.Action { ID = 14, Name = "ACTION.Action.Index", Feature = "Action", DisplayName = "ACTION.Action.Index", CreatedOn = DateTime.Now };
                Models.Action actionCreate = new Models.Action { ID = 15, Name = "ACTION.Action.Create", Feature = "Action", DisplayName = "ACTION.Action.Create", CreatedOn = DateTime.Now };
                Models.Action actionEdit = new Models.Action { ID = 16, Name = "ACTION.Action.Edit", Feature = "Action", DisplayName = "ACTION.Action.Edit", CreatedOn = DateTime.Now };

                Models.Action permissionIndex = new Models.Action { ID = 17, Name = "ACTION.Permission.Index", Feature = "Permission", DisplayName = "ACTION.Permission.Index", CreatedOn = DateTime.Now };
                Models.Action permissionCreate = new Models.Action { ID = 18, Name = "ACTION.Permission.Create", Feature = "Permission", DisplayName = "ACTION.Permission.Create", CreatedOn = DateTime.Now };
                Models.Action permissionEdit = new Models.Action { ID = 19, Name = "ACTION.Permission.Edit", Feature = "Permission", DisplayName = "ACTION.Permission.Edit", CreatedOn = DateTime.Now };

                Models.Action clearCache = new Models.Action { ID = 20, Name = "ACTION.ClearCache.Clear", Feature = "Memory cache", DisplayName = "ACTION.ClearCache.Clear", CreatedOn = DateTime.Now };

                Models.Action companyIndex = new Models.Action { ID = 21, Name = "ACTION.Company.Index", Feature = "Company", DisplayName = "ACTION.Company.Index", CreatedOn = DateTime.Now };
                Models.Action companyCreate = new Models.Action { ID = 22, Name = "ACTION.Company.Create", Feature = "Company", DisplayName = "ACTION.Company.Create", CreatedOn = DateTime.Now };
                Models.Action companyEdit = new Models.Action { ID = 23, Name = "ACTION.Company.Edit", Feature = "Company", DisplayName = "ACTION.Company.Edit", CreatedOn = DateTime.Now };
                Models.Action companyrDelete = new Models.Action { ID = 24, Name = "ACTION.Company.Delete", Feature = "Company", DisplayName = "ACTION.Company.Delete", CreatedOn = DateTime.Now };
                Models.Action companyrMapUser = new Models.Action { ID = 25, Name = "ACTION.Company.MapUser", Feature = "Company", DisplayName = "ACTION.Company.MapUser", CreatedOn = DateTime.Now };

                Models.Action changePassword = new Models.Action { ID = 26, Name = "ACTION.Account.ChangePassword", Feature = "Account", DisplayName = "ACTION.Account.ChangePassword", CreatedOn = DateTime.Now };

                Models.Action supportAndFAQIndex = new Models.Action { ID = 27, Name = "ACTION.SupportAndFAQ.Index", Feature = "Support And FAQ", DisplayName = "ACTION.SupportAndFAQ.Index", CreatedOn = DateTime.Now };
                Models.Action supportAndFAQCreate = new Models.Action { ID = 28, Name = "ACTION.SupportAndFAQ.Create", Feature = "Support And FAQ", DisplayName = "ACTION.SupportAndFAQ.Create", CreatedOn = DateTime.Now };
                Models.Action supportAndFAQEdit = new Models.Action { ID = 29, Name = "ACTION.SupportAndFAQ.Edit", Feature = "Support And FAQ", DisplayName = "ACTION.SupportAndFAQ.Edit", CreatedOn = DateTime.Now };
                Models.Action supportAndFAQDelete = new Models.Action { ID = 30, Name = "ACTION.SupportAndFAQ.Delete", Feature = "Support And FAQ", DisplayName = "ACTION.SupportAndFAQ.Delete", CreatedOn = DateTime.Now };

                Models.Action systemSettingIndex = new Models.Action { ID = 31, Name = "ACTION.SystemSetting.Index", Feature = "System Setting", DisplayName = "ACTION.SystemSetting.Index", CreatedOn = DateTime.Now };
                Models.Action systemSettingCreate = new Models.Action { ID = 32, Name = "ACTION.SystemSetting.Create", Feature = "System Setting", DisplayName = "ACTION.SystemSetting.Create", CreatedOn = DateTime.Now };
                Models.Action systemSettingEdit = new Models.Action { ID = 33, Name = "ACTION.SystemSetting.Edit", Feature = "System Setting", DisplayName = "ACTION.SystemSetting.Edit", CreatedOn = DateTime.Now };
                Models.Action systemSettingDelete = new Models.Action { ID = 34, Name = "ACTION.SystemSetting.Delete", Feature = "System Setting", DisplayName = "ACTION.SystemSetting.Delete", CreatedOn = DateTime.Now };
                Models.Action systemSettingDetails = new Models.Action { ID = 35, Name = "ACTION.SupportAndFAQ.Details", Feature = "System Setting", DisplayName = "ACTION.SystemSetting.Delete", CreatedOn = DateTime.Now };

                Models.Action cityIndex = new Models.Action { ID = 36, Name = "ACTION.City.Index", Feature = "City", DisplayName = "ACTION.City.Index", CreatedOn = DateTime.Now };
                Models.Action cityCreate = new Models.Action { ID = 37, Name = "ACTION.City.Create", Feature = "City", DisplayName = "ACTION.City.Create", CreatedOn = DateTime.Now };
                Models.Action cityEdit = new Models.Action { ID = 38, Name = "ACTION.City.Edit", Feature = "City", DisplayName = "ACTION.City.Edit", CreatedOn = DateTime.Now };
                Models.Action cityDelete = new Models.Action { ID = 39, Name = "ACTION.City.Delete", Feature = "City", DisplayName = "ACTION.City.Delete", CreatedOn = DateTime.Now };

                Models.Action areaIndex = new Models.Action { ID = 40, Name = "ACTION.AreaManager.Index", Feature = "Area Manager", DisplayName = "ACTION.AreaManager.Index", CreatedOn = DateTime.Now };
                Models.Action areaCreate = new Models.Action { ID = 41, Name = "ACTION.AreaManager.Create", Feature = "Area Manager", DisplayName = "ACTION.AreaManager.Create", CreatedOn = DateTime.Now };
                Models.Action areaEdit = new Models.Action { ID = 42, Name = "ACTION.AreaManager.Edit", Feature = "Area Manager", DisplayName = "ACTION.AreaManager.Edit", CreatedOn = DateTime.Now };
                Models.Action areaDelete = new Models.Action { ID = 43, Name = "ACTION.AreaManager.Delete", Feature = "Area Manager", DisplayName = "ACTION.AreaManager.Delete", CreatedOn = DateTime.Now };

                Models.Action imageCategoryIndex = new Models.Action { ID = 44, Name = "ACTION.ImageCategory.Index", Feature = "Image Category", DisplayName = "ACTION.ImageCategory.Index", CreatedOn = DateTime.Now };
                Models.Action imageCategoryCreate = new Models.Action { ID = 45, Name = "ACTION.ImageCategory.Create", Feature = "Image Category", DisplayName = "ACTION.ImageCategory.Create", CreatedOn = DateTime.Now };
                Models.Action imageCategoryEdit = new Models.Action { ID = 46, Name = "ACTION.ImageCategory.Edit", Feature = "Image Category", DisplayName = "ACTION.ImageCategory.Edit", CreatedOn = DateTime.Now };
                Models.Action imageCategoryDelete = new Models.Action { ID = 47, Name = "ACTION.ImageCategory.Delete", Feature = "Image Category", DisplayName = "ACTION.ImageCategory.Delete", CreatedOn = DateTime.Now };

                Models.Action galleryIndex = new Models.Action { ID = 48, Name = "ACTION.Gallery.Index", Feature = "Gallery", DisplayName = "ACTION.Gallery.Index", CreatedOn = DateTime.Now };
                Models.Action galleryCreate = new Models.Action { ID = 49, Name = "ACTION.Gallery.Create", Feature = "Gallery", DisplayName = "ACTION.Gallery.Create", CreatedOn = DateTime.Now };
                Models.Action galleryEdit = new Models.Action { ID = 50, Name = "ACTION.Gallery.Edit", Feature = "Gallery", DisplayName = "ACTION.Gallery.Edit", CreatedOn = DateTime.Now };
                Models.Action galleryDelete = new Models.Action { ID = 51, Name = "ACTION.Gallery.Delete", Feature = "Gallery", DisplayName = "ACTION.Gallery.Delete", CreatedOn = DateTime.Now };
                Models.Action galleryEditImage = new Models.Action { ID = 52, Name = "ACTION.Gallery.EditImage", Feature = "Gallery", DisplayName = "ACTION.Gallery.EditImage", CreatedOn = DateTime.Now };

                Models.Action categoryIndex = new Models.Action { ID = 53, Name = "ACTION.Category.Index", Feature = "Category", DisplayName = "ACTION.Category.Index", CreatedOn = DateTime.Now };
                Models.Action categoryCreate = new Models.Action { ID = 54, Name = "ACTION.Category.Create", Feature = "Category", DisplayName = "ACTION.Category.Create", CreatedOn = DateTime.Now };
                Models.Action categoryEdit = new Models.Action { ID = 55, Name = "ACTION.Category.Edit", Feature = "Category", DisplayName = "ACTION.Category.Edit", CreatedOn = DateTime.Now };
                Models.Action categoryDelete = new Models.Action { ID = 56, Name = "ACTION.Category.Delete", Feature = "Category", DisplayName = "ACTION.Category.Delete", CreatedOn = DateTime.Now };
                Models.Action categoryEditImage = new Models.Action { ID = 57, Name = "ACTION.Category.EditImage", Feature = "Category", DisplayName = "ACTION.Category.EditImage", CreatedOn = DateTime.Now };

                Models.Action undeliveryReasonIndex = new Models.Action { ID = 58, Name = "ACTION.UndeliveryReason.Index", Feature = "Undelivery Reason", DisplayName = "ACTION.UndeliveryReason.Index", CreatedOn = DateTime.Now };
                Models.Action undeliveryReasonCreate = new Models.Action { ID = 59, Name = "ACTION.UndeliveryReason.Create", Feature = "Undelivery Reason", DisplayName = "ACTION.UndeliveryReason.Create", CreatedOn = DateTime.Now };
                Models.Action undeliveryReasonEdit = new Models.Action { ID = 60, Name = "ACTION.UndeliveryReason.Edit", Feature = "Undelivery Reason", DisplayName = "ACTION.UndeliveryReason.Edit", CreatedOn = DateTime.Now };
                Models.Action undeliveryReasonDelete = new Models.Action { ID = 61, Name = "ACTION.UndeliveryReason.Delete", Feature = "Undelivery Reason", DisplayName = "ACTION.UndeliveryReason.Delete", CreatedOn = DateTime.Now };

                Models.Action emailIndex = new Models.Action { ID = 62, Name = "ACTION.Email.Index", Feature = "Email", DisplayName = "ACTION.Email.Index", CreatedOn = DateTime.Now };
                Models.Action emailCreate = new Models.Action { ID = 63, Name = "ACTION.Email.Create", Feature = "Email", DisplayName = "ACTION.Email.Create", CreatedOn = DateTime.Now };
                Models.Action emailEdit = new Models.Action { ID = 64, Name = "ACTION.Email.Edit", Feature = "Email", DisplayName = "ACTION.Email.Edit", CreatedOn = DateTime.Now };
                Models.Action emailDelete = new Models.Action { ID = 65, Name = "ACTION.Email.Delete", Feature = "Email", DisplayName = "ACTION.Email.Delete", CreatedOn = DateTime.Now };

                Models.Action pageManagerIndex = new Models.Action { ID = 66, Name = "ACTION.PageManager.Index", Feature = "Page Manager", DisplayName = "ACTION.PageManager.Index", CreatedOn = DateTime.Now };
                Models.Action pageManagerCreate = new Models.Action { ID = 67, Name = "ACTION.PageManager.Create", Feature = "Page Manager", DisplayName = "ACTION.PageManager.Create", CreatedOn = DateTime.Now };
                Models.Action pageManagerEdit = new Models.Action { ID = 68, Name = "ACTION.PageManager.Edit", Feature = "Page Manager", DisplayName = "ACTION.PageManager.Edit", CreatedOn = DateTime.Now };
                Models.Action pageManagerDelete = new Models.Action { ID = 69, Name = "ACTION.PageManager.Delete", Feature = "Page Manager", DisplayName = "ACTION.PageManager.Delete", CreatedOn = DateTime.Now };

                Models.Action streetIndex = new Models.Action { ID = 70, Name = "ACTION.Street.Index", Feature = "Street", DisplayName = "ACTION.Street.Index", CreatedOn = DateTime.Now };
                Models.Action streetCreate = new Models.Action { ID = 71, Name = "ACTION.Street.Create", Feature = "Street", DisplayName = "ACTION.Street.Create", CreatedOn = DateTime.Now };
                Models.Action streetEdit = new Models.Action { ID = 72, Name = "ACTION.Street.Edit", Feature = "Street", DisplayName = "ACTION.Street.Edit", CreatedOn = DateTime.Now };
                Models.Action streetDelete = new Models.Action { ID = 73, Name = "ACTION.Street.Delete", Feature = "Street", DisplayName = "ACTION.Street.Delete", CreatedOn = DateTime.Now };

                Models.Action subCategoryIndex = new Models.Action { ID = 74, Name = "ACTION.Subcategory.Index", Feature = "Sub Category", DisplayName = "ACTION.Subcategory.Index", CreatedOn = DateTime.Now };
                Models.Action subCategoryCreate = new Models.Action { ID = 75, Name = "ACTION.Subcategory.Create", Feature = "Sub Category", DisplayName = "ACTION.Subcategory.Create", CreatedOn = DateTime.Now };
                Models.Action subCategoryEdit = new Models.Action { ID = 76, Name = "ACTION.Subcategory.Edit", Feature = "Sub Category", DisplayName = "ACTION.Subcategory.Edit", CreatedOn = DateTime.Now };
                Models.Action subCategoryDelete = new Models.Action { ID = 77, Name = "ACTION.Subcategory.Delete", Feature = "Sub Category", DisplayName = "ACTION.Subcategory.Delete", CreatedOn = DateTime.Now };

                Models.Action styleClassesIndex = new Models.Action { ID = 78, Name = "ACTION.StyleClasses.Index", Feature = "Style Class", DisplayName = "ACTION.StyleClasses.Index", CreatedOn = DateTime.Now };
                Models.Action styleClassesCreate = new Models.Action { ID = 79, Name = "ACTION.StyleClasses.Create", Feature = "Style Class", DisplayName = "ACTION.StyleClasses.Create", CreatedOn = DateTime.Now };
                Models.Action styleClassesEdit = new Models.Action { ID = 80, Name = "ACTION.StyleClasses.Edit", Feature = "Style Class", DisplayName = "ACTION.StyleClasses.Edit", CreatedOn = DateTime.Now };
                Models.Action styleClassesDelete = new Models.Action { ID = 81, Name = "ACTION.StyleClasses.Delete", Feature = "Style Class", DisplayName = "ACTION.StyleClasses.Delete", CreatedOn = DateTime.Now };

                Models.Action styleTraitsIndex = new Models.Action { ID = 82, Name = "ACTION.StyleTraits.Index", Feature = "Style Traits", DisplayName = "ACTION.StyleTraits.Index", CreatedOn = DateTime.Now };
                Models.Action styleTraitsCreate = new Models.Action { ID = 83, Name = "ACTION.StyleTraits.Create", Feature = "Style Traits", DisplayName = "ACTION.StyleTraits.Create", CreatedOn = DateTime.Now };
                Models.Action styleTraitsEdit = new Models.Action { ID = 84, Name = "ACTION.StyleTraits.Edit", Feature = "Style Traits", DisplayName = "ACTION.StyleTraits.Edit", CreatedOn = DateTime.Now };
                Models.Action styleTraitsDelete = new Models.Action { ID = 85, Name = "ACTION.StyleTraits.Delete", Feature = "Style Traits", DisplayName = "ACTION.StyleTraits.Delete", CreatedOn = DateTime.Now };

                Models.Action styleTraitValuesIndex = new Models.Action { ID = 86, Name = "ACTION.StyleTraitValues.Index", Feature = "Style Trait Values", DisplayName = "ACTION.StyleTraitValues.Index", CreatedOn = DateTime.Now };
                Models.Action styleTraitValuesCreate = new Models.Action { ID = 87, Name = "ACTION.StyleTraitValues.Create", Feature = "Style Traits Values", DisplayName = "ACTION.StyleTraitValues.Create", CreatedOn = DateTime.Now };
                Models.Action styleTraitValuesEdit = new Models.Action { ID = 88, Name = "ACTION.StyleTraitValues.Edit", Feature = "Style Trait Values", DisplayName = "ACTION.StyleTraitValues.Edit", CreatedOn = DateTime.Now };
                Models.Action styleTraitValuesDelete = new Models.Action { ID = 89, Name = "ACTION.StyleTraitValues.Delete", Feature = "Style Trait Values", DisplayName = "ACTION.StyleTraitValues.Delete", CreatedOn = DateTime.Now };

                Models.Action uomIndex = new Models.Action { ID = 90, Name = "ACTION.UnitOfMeasure.Index", Feature = "Unit of Measure", DisplayName = "ACTION.UnitOfMeasure.Index", CreatedOn = DateTime.Now };
                Models.Action uomCreate = new Models.Action { ID = 91, Name = "ACTION.UnitOfMeasure.Create", Feature = "Unit of Measure", DisplayName = "ACTION.UnitOfMeasure.Create", CreatedOn = DateTime.Now };
                Models.Action uomEdit = new Models.Action { ID = 92, Name = "ACTION.UnitOfMeasure.Edit", Feature = "Unit of Measure", DisplayName = "ACTION.UnitOfMeasure.Edit", CreatedOn = DateTime.Now };
                Models.Action uomDelete = new Models.Action { ID = 93, Name = "ACTION.UnitOfMeasure.Delete", Feature = "Unit of Measure", DisplayName = "ACTION.UnitOfMeasure.Delete", CreatedOn = DateTime.Now };

                Models.Action productIndex = new Models.Action { ID = 94, Name = "ACTION.Product.Index", Feature = "Product", DisplayName = "ACTION.Product.Index", CreatedOn = DateTime.Now };
                Models.Action productManage = new Models.Action { ID = 95, Name = "ACTION.Product.Manage", Feature = "Product", DisplayName = "ACTION.Product.Manage", CreatedOn = DateTime.Now };
                Models.Action productCategory = new Models.Action { ID = 96, Name = "ACTION.Product.Category", Feature = "Product", DisplayName = "ACTION.Product.Category", CreatedOn = DateTime.Now };
                Models.Action productDiscount = new Models.Action { ID = 97, Name = "ACTION.Product.Discount", Feature = "Product", DisplayName = "ACTION.Product.Discount", CreatedOn = DateTime.Now };
                Models.Action productImages = new Models.Action { ID = 98, Name = "ACTION.Product.Images", Feature = "Product", DisplayName = "ACTION.Product.Images", CreatedOn = DateTime.Now };
                Models.Action productProductVariant = new Models.Action { ID = 99, Name = "ACTION.Product.ProductVariant", Feature = "Product", DisplayName = "ACTION.Product.ProductVariant", CreatedOn = DateTime.Now };
                Models.Action productDDStyleTraitByStyleClassId = new Models.Action { ID = 100, Name = "ACTION.Product.DDStyleTraitByStyleClassId", Feature = "Product", DisplayName = "ACTION.Product.DDStyleTraitByStyleClassId", CreatedOn = DateTime.Now };
                Models.Action productDDStyleTraitValueByStyleTraitId = new Models.Action { ID = 101, Name = "ACTION.Product.DDStyleTraitValueByStyleTraitId", Feature = "Product", DisplayName = "ACTION.Product.DDStyleTraitValueByStyleTraitId", CreatedOn = DateTime.Now };
                Models.Action productDDSubcategoryByCategoryId = new Models.Action { ID = 102, Name = "ACTION.Product.DDSubcategoryByCategoryId", Feature = "Product", DisplayName = "ACTION.Product.DDSubcategoryByCategoryId", CreatedOn = DateTime.Now };

                Models.Action homeError = new Models.Action { ID = 103, Name = "ACTION.Home.Error", Feature = "Home", DisplayName = "ACTION.Home.Error", CreatedOn = DateTime.Now };

                Models.Action couponIndex = new Models.Action { ID = 104, Name = "ACTION.Coupon.Index", Feature = "Coupon", DisplayName = "ACTION.Coupon.Index", CreatedOn = DateTime.Now };
                Models.Action couponCreate = new Models.Action { ID = 105, Name = "ACTION.Coupon.Create", Feature = "Coupon", DisplayName = "ACTION.Coupon.Create", CreatedOn = DateTime.Now };
                Models.Action couponEdit = new Models.Action { ID = 106, Name = "ACTION.Coupon.Edit", Feature = "Coupon", DisplayName = "ACTION.Coupon.Edit", CreatedOn = DateTime.Now };
                Models.Action couponDelete = new Models.Action { ID = 107, Name = "ACTION.Coupon.Delete", Feature = "Coupon", DisplayName = "ACTION.Coupon.Delete", CreatedOn = DateTime.Now };

                Models.Action storeIndex = new Models.Action { ID = 108, Name = "ACTION.Store.Index", Feature = "Store", DisplayName = "ACTION.Store.Index", CreatedOn = DateTime.Now };
                Models.Action storeCreate = new Models.Action { ID = 109, Name = "ACTION.Store.Create", Feature = "Store", DisplayName = "ACTION.Store.Create", CreatedOn = DateTime.Now };
                Models.Action storeEdit = new Models.Action { ID = 110, Name = "ACTION.Store.Edit", Feature = "Store", DisplayName = "ACTION.Store.Edit", CreatedOn = DateTime.Now };
                Models.Action storeDelete = new Models.Action { ID = 111, Name = "ACTION.Store.Delete", Feature = "Store", DisplayName = "ACTION.Store.Delete", CreatedOn = DateTime.Now };

                Models.Action pushNotificationIndex = new Models.Action { ID = 112, Name = "ACTION.PushNotification.Index", Feature = "PushNotification", DisplayName = "ACTION.PushNotification.Index", CreatedOn = DateTime.Now };
                Models.Action pushNotificationCreate = new Models.Action { ID = 113, Name = "ACTION.PushNotification.SendNotification", Feature = "PushNotification", DisplayName = "ACTION.PushNotification.SendNotification", CreatedOn = DateTime.Now };

                Models.Action timeSlotIndex = new Models.Action { ID = 114, Name = "ACTION.TimeSlot.Index", Feature = "TimeSlot", DisplayName = "ACTION.TimeSlot.Index", CreatedOn = DateTime.Now };
                Models.Action timeSlotCreate = new Models.Action { ID = 115, Name = "ACTION.TimeSlot.Create", Feature = "TimeSlot", DisplayName = "ACTION.TimeSlot.Create", CreatedOn = DateTime.Now };
                Models.Action timeSlotEdit = new Models.Action { ID = 116, Name = "ACTION.TimeSlot.Edit", Feature = "TimeSlot", DisplayName = "ACTION.TimeSlot.Edit", CreatedOn = DateTime.Now };

                Models.Action appMessageIndex = new Models.Action { ID = 117, Name = "ACTION.AppMessage.Index", Feature = "AppMessage", DisplayName = "ACTION.AppMessage.Index", CreatedOn = DateTime.Now };
                Models.Action appMessageCreate = new Models.Action { ID = 118, Name = "ACTION.AppMessage.Create", Feature = "AppMessage", DisplayName = "ACTION.AppMessage.Create", CreatedOn = DateTime.Now };
                Models.Action appMessageEdit = new Models.Action { ID = 119, Name = "ACTION.AppMessage.Edit", Feature = "AppMessage", DisplayName = "ACTION.AppMessage.Edit", CreatedOn = DateTime.Now };
                Models.Action appMessageDelete = new Models.Action { ID = 120, Name = "ACTION.AppMessage.Delete", Feature = "AppMessage", DisplayName = "ACTION.AppMessage.Delete", CreatedOn = DateTime.Now };

                Models.Action deliveryBoyIndex = new Models.Action { ID = 121, Name = "ACTION.DeliveryBoy.Index", Feature = "DeliveryBoy", DisplayName = "ACTION.DeliveryBoy.Index", CreatedOn = DateTime.Now };
                Models.Action deliveryBoyCreate = new Models.Action { ID = 122, Name = "ACTION.DeliveryBoy.Create", Feature = "DeliveryBoy", DisplayName = "ACTION.DeliveryBoy.Create", CreatedOn = DateTime.Now };
                Models.Action deliveryBoyEdit = new Models.Action { ID = 123, Name = "ACTION.DeliveryBoy.Edit", Feature = "DeliveryBoy", DisplayName = "ACTION.DeliveryBoy.Edit", CreatedOn = DateTime.Now };
                Models.Action deliveryBoyDelete = new Models.Action { ID = 124, Name = "ACTION.DeliveryBoy.Delete", Feature = "DeliveryBoy", DisplayName = "ACTION.DeliveryBoy.Delete", CreatedOn = DateTime.Now };

                Models.Action orderIndex = new Models.Action { ID = 125, Name = "ACTION.Orders.Index", Feature = "Orders", DisplayName = "ACTION.Orders.Index", CreatedOn = DateTime.Now };
                Models.Action orderInstaService = new Models.Action { ID = 126, Name = "ACTION.Orders.InstaService", Feature = "Orders", DisplayName = "ACTION.Orders.InstaService", CreatedOn = DateTime.Now };
                Models.Action orderScheduleService = new Models.Action { ID = 127, Name = "ACTION.Orders.ScheduleService", Feature = "Orders", DisplayName = "ACTION.Orders.ScheduleService", CreatedOn = DateTime.Now };
                Models.Action orderAssigned = new Models.Action { ID = 128, Name = "ACTION.Orders.Assigned", Feature = "Orders", DisplayName = "ACTION.Orders.Assigned", CreatedOn = DateTime.Now };
                Models.Action orderCompleted = new Models.Action { ID = 129, Name = "ACTION.Orders.Completed", Feature = "Orders", DisplayName = "ACTION.Orders.Completed", CreatedOn = DateTime.Now };
                Models.Action orderDetails = new Models.Action { ID = 130, Name = "ACTION.Orders.Details", Feature = "Orders", DisplayName = "ACTION.Orders.Details", CreatedOn = DateTime.Now };
                Models.Action orderAssign = new Models.Action { ID = 131, Name = "ACTION.Orders.Assign", Feature = "Orders", DisplayName = "ACTION.Orders.Assign", CreatedOn = DateTime.Now };
                Models.Action orderMarkAsDelivered = new Models.Action { ID = 132, Name = "ACTION.Orders.MarkAsDelivered", Feature = "Orders", DisplayName = "ACTION.Orders.MarkAsDelivered", CreatedOn = DateTime.Now };

                Models.Action customerIndex = new Models.Action { ID = 133, Name = "ACTION.Customer.Index", Feature = "Customer", DisplayName = "ACTION.Orders.Index", CreatedOn = DateTime.Now };
                Models.Action customerAssignCredits = new Models.Action { ID = 134, Name = "ACTION.Customer.AssignCredits", Feature = "Customer", DisplayName = "ACTION.Customer.AssignCredits", CreatedOn = DateTime.Now };

                Models.Action returnCanIndex = new Models.Action { ID = 135, Name = "ACTION.ReturnCan.Index", Feature = "ReturnCan", DisplayName = "ACTION.ReturnCan.Index", CreatedOn = DateTime.Now };
                Models.Action returnCanAssigned = new Models.Action { ID = 136, Name = "ACTION.ReturnCan.Assigned", Feature = "ReturnCan", DisplayName = "ACTION.ReturnCan.Assigned", CreatedOn = DateTime.Now };
                Models.Action returnCanCompleted = new Models.Action { ID = 137, Name = "ACTION.ReturnCan.Completed", Feature = "ReturnCan", DisplayName = "ACTION.ReturnCan.Completed", CreatedOn = DateTime.Now };
                Models.Action returnCanDetails = new Models.Action { ID = 138, Name = "ACTION.ReturnCan.Details", Feature = "ReturnCan", DisplayName = "ACTION.ReturnCan.Details", CreatedOn = DateTime.Now };
                Models.Action returnCanAssignToPickUp = new Models.Action { ID = 139, Name = "ACTION.ReturnCan.AssignToPickUp", Feature = "ReturnCan", DisplayName = "ACTION.ReturnCan.AssignToPickUp", CreatedOn = DateTime.Now };

                Models.Action deliveryIndex = new Models.Action { ID = 140, Name = "ACTION.Delivery.Index", Feature = "Delivery", DisplayName = "ACTION.Delivery.Index", CreatedOn = DateTime.Now };
                Models.Action deliveryCompletedDelivery = new Models.Action { ID = 141, Name = "ACTION.Delivery.CompletedDelivery", Feature = "Delivery", DisplayName = "ACTION.Delivery.CompletedDelivery", CreatedOn = DateTime.Now };
                Models.Action deliveryDetails = new Models.Action { ID = 142, Name = "ACTION.Delivery.Details", Feature = "Delivery", DisplayName = "ACTION.Delivery.Details", CreatedOn = DateTime.Now };
                Models.Action deliveryMarkAsDelivered = new Models.Action { ID = 143, Name = "ACTION.Delivery.MarkAsDelivered", Feature = "Delivery", DisplayName = "ACTION.Delivery.MarkAsDelivered", CreatedOn = DateTime.Now };
                Models.Action deliveryMarkedAsPickUp = new Models.Action { ID = 144, Name = "ACTION.Delivery.MarkedAsPickUp", Feature = "Delivery", DisplayName = "ACTION.Delivery.MarkedAsPickUp", CreatedOn = DateTime.Now };
                Models.Action deliveryAssignedReturnCan = new Models.Action { ID = 145, Name = "ACTION.Delivery.AssignedReturnCan", Feature = "Delivery", DisplayName = "ACTION.Delivery.AssignedReturnCan", CreatedOn = DateTime.Now };
                Models.Action deliveryCompletedReturncan = new Models.Action { ID = 146, Name = "ACTION.Delivery.CompletedReturncan", Feature = "Delivery", DisplayName = "ACTION.Delivery.CompletedReturncan", CreatedOn = DateTime.Now };
                Models.Action deliveryReturnCanDetails = new Models.Action { ID = 147, Name = "ACTION.Delivery.ReturnCanDetails", Feature = "Delivery", DisplayName = "ACTION.Delivery.ReturnCanDetails", CreatedOn = DateTime.Now };

                Models.Action orderExport = new Models.Action { ID = 148, Name = "ACTION.Orders.Export", Feature = "Orders", DisplayName = "ACTION.Orders.Export", CreatedOn = DateTime.Now };
                Models.Action orderCancel = new Models.Action { ID = 149, Name = "ACTION.Orders.Cancel", Feature = "Orders", DisplayName = "ACTION.Orders.Cancel", CreatedOn = DateTime.Now };
                Models.Action orderCancelled = new Models.Action { ID = 150, Name = "ACTION.Orders.Cancelled", Feature = "Orders", DisplayName = "ACTION.Orders.Cancelled", CreatedOn = DateTime.Now };

                Models.Action vendorIndex = new Models.Action { ID = 151, Name = "ACTION.Vendor.Index", Feature = "Vendor", DisplayName = "ACTION.Orders.Index", CreatedOn = DateTime.Now };
                Models.Action vendorCreate = new Models.Action { ID = 152, Name = "ACTION.Vendor.Create", Feature = "Vendor", DisplayName = "ACTION.Vendor.Create", CreatedOn = DateTime.Now };
                Models.Action vendorEdit = new Models.Action { ID = 153, Name = "ACTION.Vendor.Edit", Feature = "Vendor", DisplayName = "ACTION.Vendor.Edit", CreatedOn = DateTime.Now };

                Models.Action ServiceTypeIndex = new Models.Action { ID = 154, Name = "ACTION.ServiceType.Index", Feature = "ServiceType", DisplayName = "ACTION.ServiceType.Index", CreatedOn = DateTime.Now };
                Models.Action ServiceTypeCreate = new Models.Action { ID = 155, Name = "ACTION.ServiceType.Create", Feature = "ServiceType", DisplayName = "ACTION.ServiceType.Create", CreatedOn = DateTime.Now };
                Models.Action ServiceTypeEdit = new Models.Action { ID = 156, Name = "ACTION.ServiceType.Edit", Feature = "ServiceType", DisplayName = "ACTION.ServiceType.Edit", CreatedOn = DateTime.Now };
                Models.Action serviceTypeDelete = new Models.Action { ID = 157, Name = "ACTION.ServiceType.Delete", Feature = "ServiceType", DisplayName = "ACTION.ServiceType.Delete", CreatedOn = DateTime.Now };

                Models.Action clothsIndex = new Models.Action { ID = 158, Name = "ACTION.Cloths.Index", Feature = "Cloths", DisplayName = "ACTION.Cloths.Index", CreatedOn = DateTime.Now };
                Models.Action clothsCreate = new Models.Action { ID = 159, Name = "ACTION.Cloths.Create", Feature = "Cloths", DisplayName = "ACTION.Cloths.Create", CreatedOn = DateTime.Now };
                Models.Action clothsEdit = new Models.Action { ID = 160, Name = "ACTION.Cloths.Edit", Feature = "Cloths", DisplayName = "ACTION.Cloths.Edit", CreatedOn = DateTime.Now };
                Models.Action clothsDelete = new Models.Action { ID = 161, Name = "ACTION.Cloths.Delete", Feature = "Cloths", DisplayName = "ACTION.Cloths.Delete", CreatedOn = DateTime.Now };

                Models.Action rateCardIndex = new Models.Action { ID = 162, Name = "ACTION.RateCard.Index", Feature = "RateCard", DisplayName = "ACTION.RateCard.Index", CreatedOn = DateTime.Now };
                Models.Action rateCardCreate = new Models.Action { ID = 163, Name = "ACTION.RateCard.Create", Feature = "RateCard", DisplayName = "ACTION.RateCard.Create", CreatedOn = DateTime.Now };
                Models.Action rateCardEdit = new Models.Action { ID = 164, Name = "ACTION.RateCard.Edit", Feature = "RateCard", DisplayName = "ACTION.RateCard.Edit", CreatedOn = DateTime.Now };
                Models.Action rateCardDelete = new Models.Action { ID = 165, Name = "ACTION.RateCard.Delete", Feature = "RateCard", DisplayName = "ACTION.RateCard.Delete", CreatedOn = DateTime.Now };

                Models.Action packageIndex = new Models.Action { ID = 166, Name = "ACTION.Package.Index", Feature = "Package", DisplayName = "ACTION.Package.Index", CreatedOn = DateTime.Now };
                Models.Action packageCreate = new Models.Action { ID = 167, Name = "ACTION.Package.Create", Feature = "Package", DisplayName = "ACTION.Package.Create", CreatedOn = DateTime.Now };
                Models.Action packageEdit = new Models.Action { ID = 168, Name = "ACTION.Package.Edit", Feature = "Package", DisplayName = "ACTION.Package.Edit", CreatedOn = DateTime.Now };
                Models.Action packageDelete = new Models.Action { ID = 169, Name = "ACTION.Package.Delete", Feature = "Package", DisplayName = "ACTION.Package.Delete", CreatedOn = DateTime.Now };

                Models.Action customerAddresses = new Models.Action { ID = 170, Name = "ACTION.Customer.Addresses", Feature = "Customer", DisplayName = "ACTION.Customer.Addresses", CreatedOn = DateTime.Now };
                Models.Action customerAddressCreate = new Models.Action { ID = 171, Name = "ACTION.Customer.AddressCreate", Feature = "Customer", DisplayName = "ACTION.Customer.AddressCreate", CreatedOn = DateTime.Now };
                Models.Action customerMapAddress = new Models.Action { ID = 172, Name = "ACTION.Customer.MapAddress", Feature = "Customer", DisplayName = "ACTION.Customer.MapAddress", CreatedOn = DateTime.Now };

                Models.Action pushNotificationReminder = new Models.Action { ID = 173, Name = "ACTION.PushNotification.Reminder", Feature = "PushNotification", DisplayName = "ACTION.PushNotification.Reminder", CreatedOn = DateTime.Now };

                Models.Action orderOneTouch = new Models.Action { ID = 174, Name = "ACTION.Orders.OneTouch", Feature = "Orders", DisplayName = "ACTION.Orders.OneTouch", CreatedOn = DateTime.Now };
                Models.Action orderWhatsApp = new Models.Action { ID = 175, Name = "ACTION.Orders.WhatsApp", Feature = "Orders", DisplayName = "ACTION.Orders.WhatsApp", CreatedOn = DateTime.Now };
                Models.Action orderCustomize = new Models.Action { ID = 176, Name = "ACTION.Orders.Customize", Feature = "Orders", DisplayName = "ACTION.Orders.Customize", CreatedOn = DateTime.Now };
                Models.Action orderPackage = new Models.Action { ID = 177, Name = "ACTION.Orders.Package", Feature = "Orders", DisplayName = "ACTION.Orders.Package", CreatedOn = DateTime.Now };
                Models.Action orderSendEmail = new Models.Action { ID = 178, Name = "ACTION.Orders.SendEmail", Feature = "Orders", DisplayName = "ACTION.Orders.SendEmail", CreatedOn = DateTime.Now };
                Models.Action orderOrderListItem = new Models.Action { ID = 179, Name = "ACTION.Orders.OrderListItem", Feature = "Orders", DisplayName = "ACTION.Orders.OrderListItem", CreatedOn = DateTime.Now };
                Models.Action orderMarkAsPickedUp = new Models.Action { ID = 180, Name = "ACTION.Orders.MarkAsPickedUp", Feature = "Orders", DisplayName = "ACTION.Orders.MarkAsPickedUp", CreatedOn = DateTime.Now };
                Models.Action orderAssignToVendor = new Models.Action { ID = 181, Name = "ACTION.Orders.AssignToVendor", Feature = "Orders", DisplayName = "ACTION.Orders.AssignToVendor", CreatedOn = DateTime.Now };
                Models.Action orderPickupFromVendor = new Models.Action { ID = 182, Name = "ACTION.Orders.PickupFromVendor", Feature = "Orders", DisplayName = "ACTION.Orders.PickupFromVendor", CreatedOn = DateTime.Now };
                Models.Action orderAssignForDelviery = new Models.Action { ID = 183, Name = "ACTION.Orders.AssignForDelviery", Feature = "Orders", DisplayName = "ACTION.Orders.AssignForDelviery", CreatedOn = DateTime.Now };
                Models.Action orderMarkDelivered = new Models.Action { ID = 184, Name = "ACTION.Orders.MarkDelivered", Feature = "Orders", DisplayName = "ACTION.Orders.MarkDelivered", CreatedOn = DateTime.Now };
                //reports
                Models.Action reportsOrderReport = new Models.Action { ID = 185, Name = "ACTION.Reports.OrderReport", Feature = "Reports", DisplayName = "ACTION.Reports.OrderReport", CreatedOn = DateTime.Now };
                Models.Action reportsProfitAndLoss = new Models.Action { ID = 186, Name = "ACTION.Reports.ProfitAndLoss", Feature = "Reports", DisplayName = "ACTION.Reports.ProfitAndLoss", CreatedOn = DateTime.Now };
                //HomeScreenLayout icons
                Models.Action homeScreenLayoutIndex = new Models.Action { ID = 187, Name = "ACTION.HomeScreenLayout.Index", Feature = "HomeScreenLayout", DisplayName = "ACTION.HomeScreenLayout.Index", CreatedOn = DateTime.Now };
                Models.Action homeScreenLayoutEdit = new Models.Action { ID = 188, Name = "ACTION.HomeScreenLayout.Edit", Feature = "HomeScreenLayout", DisplayName = "ACTION.HomeScreenLayout.Edit", CreatedOn = DateTime.Now };

                Models.Action classifiedIndex = new Models.Action { ID = 189, Name = "ACTION.AddNewClassified.Index", Feature = "Classified", DisplayName = "ACTION.AddNewClassified.Index", CreatedOn = DateTime.Now };
                Models.Action classifiedAdd = new Models.Action { ID = 190, Name = "ACTION.AddNewClassified.AddClassified", Feature = "Classified", DisplayName = "ACTION.AddNewClassified.AddClassified", CreatedOn = DateTime.Now };

                Models.Action classifiedListIndex = new Models.Action { ID = 191, Name = "ACTION.ClassifiedList.Index", Feature = "Classified", DisplayName = "ACTION.ClassifiedList.Index", CreatedOn = DateTime.Now };
                Models.Action classifiedListEdit = new Models.Action { ID = 192, Name = "ACTION.ClassifiedList.Edit", Feature = "Classified", DisplayName = "ACTION.ClassifiedList.Edit", CreatedOn = DateTime.Now };
                Models.Action classifiedListDelete = new Models.Action { ID = 193, Name = "ACTION.ClassifiedList.Delete", Feature = "Classified", DisplayName = "ACTION.ClassifiedList.Delete", CreatedOn = DateTime.Now };

                Models.Action carouselIndex = new Models.Action { ID = 194, Name = "ACTION.Carousel.Index", Feature = "Carousel", DisplayName = "ACTION.Carousel.Index", CreatedOn = DateTime.Now };
                Models.Action carouselCreate = new Models.Action { ID = 195, Name = "ACTION.Carousel.Create", Feature = "Carousel", DisplayName = "ACTION.Carousel.Create", CreatedOn = DateTime.Now };
                Models.Action carouselEdit = new Models.Action { ID = 196, Name = "ACTION.Carousel.Edit", Feature = "Carousel", DisplayName = "ACTION.Carousel.Edit", CreatedOn = DateTime.Now };
                Models.Action carouselDelete = new Models.Action { ID = 197, Name = "ACTION.Carousel.Delete", Feature = "Carousel", DisplayName = "ACTION.Carousel.Delete", CreatedOn = DateTime.Now };

                Models.Action classifiedArticleIndex = new Models.Action { ID = 198, Name = "ACTION.ClassifiedArticle.Index", Feature = "ClassifiedArticle", DisplayName = "ACTION.ClassifiedArticle.Index", CreatedOn = DateTime.Now };
                Models.Action classifiedArticleCreate = new Models.Action { ID = 199, Name = "ACTION.ClassifiedArticle.Create", Feature = "ClassifiedArticle", DisplayName = "ACTION.ClassifiedArticle.Create", CreatedOn = DateTime.Now };
                Models.Action classifiedArticleEdit = new Models.Action { ID = 200, Name = "ACTION.ClassifiedArticle.Edit", Feature = "ClassifiedArticle", DisplayName = "ACTION.ClassifiedArticle.Edit", CreatedOn = DateTime.Now };
                Models.Action classifiedArticleDelete = new Models.Action { ID = 201, Name = "ACTION.ClassifiedArticle.Delete", Feature = "ClassifiedArticle", DisplayName = "ACTION.ClassifiedArticle.Delete", CreatedOn = DateTime.Now };

                context.Action.AddOrUpdate(a => a.ID,
                    homeIndex,

                    roleIndex,
                    roleCreate,
                    roleEdit,
                    roleDelete,

                    accountLogOff,

                    userIndex,
                    userCreate,
                    userEdit,
                    userDelete,

                    menuIndex,
                    menuCreate,
                    menuEdit,

                    actionIndex,
                    actionCreate,
                    actionEdit,

                    permissionIndex,
                    permissionCreate,
                    permissionEdit,

                    clearCache,

                    companyIndex,
                    companyCreate,
                    companyEdit,
                    companyrDelete,
                    companyrMapUser,

                    changePassword,

                    supportAndFAQIndex,
                    supportAndFAQCreate,
                    supportAndFAQEdit,
                    supportAndFAQDelete,

                    systemSettingIndex,
                    systemSettingCreate,
                    systemSettingEdit,
                    systemSettingDelete,
                    systemSettingDetails,

                    cityIndex,
                    cityCreate,
                    cityEdit,
                    cityDelete,

                    areaIndex,
                    areaCreate,
                    areaEdit,
                    areaDelete,

                    imageCategoryIndex,
                    imageCategoryCreate,
                    imageCategoryEdit,
                    imageCategoryDelete,

                    galleryIndex,
                    galleryCreate,
                    galleryEdit,
                    galleryDelete,
                    galleryEditImage,

                    categoryIndex,
                    categoryCreate,
                    categoryEdit,
                    categoryDelete,
                    categoryEditImage,

                    undeliveryReasonIndex,
                    undeliveryReasonCreate,
                    undeliveryReasonEdit,
                    undeliveryReasonDelete,

                    emailIndex,
                    emailCreate,
                    emailEdit,
                    emailDelete,

                    pageManagerIndex,
                    pageManagerCreate,
                    pageManagerEdit,
                    pageManagerDelete,

                    streetIndex,
                    streetCreate,
                    streetEdit,
                    streetDelete,

                    subCategoryIndex,
                    subCategoryCreate,
                    subCategoryEdit,
                    subCategoryDelete,

                    styleClassesIndex,
                    styleClassesCreate,
                    styleClassesEdit,
                    styleClassesDelete,

                    styleTraitsIndex,
                    styleTraitsCreate,
                    styleTraitsEdit,
                    styleTraitsDelete,

                    styleTraitValuesIndex,
                    styleTraitValuesCreate,
                    styleTraitValuesEdit,
                    styleTraitValuesDelete,

                    uomIndex,
                    uomCreate,
                    uomEdit,
                    uomDelete,

                    productIndex,
                    productManage,
                    productCategory,
                    productDiscount,
                    productImages,
                    productProductVariant,
                    productDDStyleTraitByStyleClassId,
                    productDDStyleTraitValueByStyleTraitId,
                    productDDSubcategoryByCategoryId,
                    homeError,

                    couponIndex,
                    couponCreate,
                    couponEdit,
                    couponDelete,

                    storeIndex,
                    storeCreate,
                    storeEdit,
                    storeDelete,

                    pushNotificationIndex,
                    pushNotificationCreate,

                    timeSlotIndex,
                    timeSlotCreate,
                    timeSlotEdit,

                    appMessageIndex,
                    appMessageCreate,
                    appMessageEdit,
                    appMessageDelete,

                    deliveryBoyIndex,
                    deliveryBoyCreate,
                    deliveryBoyEdit,
                    deliveryBoyDelete,

                    orderIndex,
                    orderInstaService,
                    orderScheduleService,
                    orderAssigned,
                    orderCompleted,
                    orderDetails,
                    orderAssign,
                    orderMarkAsDelivered,
                    customerIndex,
                    customerAssignCredits,

                    returnCanIndex,
                    returnCanAssigned,
                    returnCanCompleted,
                    returnCanDetails,
                    returnCanAssignToPickUp,

                    deliveryIndex,
                    deliveryCompletedDelivery,
                    deliveryDetails,
                    deliveryMarkAsDelivered,
                    deliveryMarkedAsPickUp,
                    deliveryAssignedReturnCan,
                    deliveryCompletedReturncan,
                    deliveryReturnCanDetails,

                    orderExport,
                    orderCancel,
                    orderCancelled,

                    vendorIndex,
                    vendorCreate,
                    vendorEdit,

                    ServiceTypeIndex,
                    ServiceTypeCreate,
                    ServiceTypeEdit,
                    serviceTypeDelete,

                    clothsIndex,
                    clothsCreate,
                    clothsEdit,
                    clothsDelete,

                    rateCardIndex,
                    rateCardCreate,
                    rateCardEdit,
                    rateCardDelete,

                    packageIndex,
                    packageCreate,
                    packageEdit,
                    packageDelete,

                    customerAddresses,
                    customerAddressCreate,
                    customerMapAddress,

                    pushNotificationReminder,

                    orderOneTouch,
                    orderWhatsApp,
                    orderCustomize,
                    orderPackage,
                    orderSendEmail,
                    orderOrderListItem,
                    orderMarkAsPickedUp,
                    orderAssignToVendor,
                    orderPickupFromVendor,
                    orderAssignForDelviery,
                    orderMarkDelivered,
                    reportsOrderReport,
                    reportsProfitAndLoss,

                    homeScreenLayoutIndex,
                    homeScreenLayoutEdit,

                    classifiedIndex,
                    classifiedAdd,

                    classifiedListIndex,
                    classifiedListEdit,
                    classifiedListDelete,

                    carouselIndex,
                    carouselCreate,
                    carouselEdit,
                    carouselDelete,

                    classifiedArticleIndex,
                    classifiedArticleCreate,
                    classifiedArticleEdit,
                    classifiedArticleDelete
                    );

                Models.Permission viewHome = new Models.Permission { ID = 1, Name = "View home", Feature = "Home", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { homeIndex, accountLogOff, changePassword, homeError } };

                Models.Permission viewRole = new Models.Permission { ID = 2, Name = "View role", Feature = "Role", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { roleIndex } };
                Models.Permission manageRole = new Models.Permission
                {
                    ID = 3,
                    Name = "Manage role",
                    Feature = "Role",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    roleCreate,
                    roleDelete,
                    roleEdit
                 }
                };

                Models.Permission viewUser = new Models.Permission { ID = 4, Name = "View user", Feature = "User", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { userIndex } };
                Models.Permission manageUser = new Models.Permission
                {
                    ID = 5,
                    Name = "Manage user",
                    Feature = "User",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                userCreate,
                userEdit,
                userDelete}
                };

                Models.Permission viewMenu = new Models.Permission { ID = 6, Name = "View menu", Feature = "Menu", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { menuIndex } };
                Models.Permission manageMenu = new Models.Permission
                {
                    ID = 7,
                    Name = "Manage menu",
                    Feature = "Menu",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                menuCreate,
                menuEdit
                }
                };

                Models.Permission viewAction = new Models.Permission { ID = 8, Name = "View action", Feature = "Action", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { actionIndex } };
                Models.Permission manageAction = new Models.Permission
                {
                    ID = 9,
                    Name = "Manage action",
                    Feature = "Action",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                actionCreate,
                actionEdit
                }
                };

                Models.Permission viewPermission = new Models.Permission { ID = 10, Name = "View permission", Feature = "Permission", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { permissionIndex } };
                Models.Permission managePermission = new Models.Permission
                {
                    ID = 11,
                    Name = "Manage permission",
                    Feature = "Permission",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                permissionCreate,
                permissionEdit
                }
                };

                Models.Permission manageClearCache = new Models.Permission { ID = 12, Name = "Manage Cache", Feature = "Memory cache", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { clearCache } };

                Models.Permission viewCompany = new Models.Permission { ID = 13, Name = "View company", Feature = "Company", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { companyIndex } };
                Models.Permission manageCompany = new Models.Permission
                {
                    ID = 14,
                    Name = "Manage company",
                    Feature = "Company",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                companyCreate,
                companyEdit,
                companyrDelete,
                companyrMapUser
                }
                };

                Models.Permission viewSupportFAQ = new Models.Permission { ID = 15, Name = "View spport and faq", Feature = "Support And FAQ", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { supportAndFAQIndex } };
                Models.Permission manageSupportFAQ = new Models.Permission
                {
                    ID = 16,
                    Name = "Manage spport and faq",
                    Feature = "Support And FAQ",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    supportAndFAQCreate,
                    supportAndFAQEdit,
                    supportAndFAQDelete
                }
                };

                Models.Permission viewSystemSettings = new Models.Permission { ID = 17, Name = "View system settings", Feature = "System settings", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { systemSettingDetails, systemSettingIndex } };
                Models.Permission manageSystemSettings = new Models.Permission
                {
                    ID = 18,
                    Name = "Manage system settings",
                    Feature = "System settings",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    systemSettingCreate,
                    systemSettingEdit,
                    systemSettingDelete
                }
                };

                Models.Permission viewCity = new Models.Permission { ID = 19, Name = "View city", Feature = "City", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { cityIndex } };
                Models.Permission manageCity = new Models.Permission
                {
                    ID = 20,
                    Name = "Manage city",
                    Feature = "City",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    cityCreate,
                    cityEdit,
                    cityDelete
                }
                };

                Models.Permission viewArea = new Models.Permission { ID = 21, Name = "View area", Feature = "Area Manager", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { areaIndex } };
                Models.Permission manageArea = new Models.Permission
                {
                    ID = 22,
                    Name = "Manage area",
                    Feature = "Area Manager",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    areaCreate,
                    areaEdit,
                    areaDelete
                }
                };

                Models.Permission viewImageCategory = new Models.Permission { ID = 23, Name = "View image category", Feature = "Image Category", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { imageCategoryIndex } };
                Models.Permission manageImageCategory = new Models.Permission
                {
                    ID = 24,
                    Name = "Manage image category",
                    Feature = "Image Category",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    imageCategoryCreate,
                    imageCategoryEdit,
                    imageCategoryDelete
                }
                };

                Models.Permission viewGallery = new Models.Permission { ID = 25, Name = "View gallery", Feature = "Gallery", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { galleryIndex } };
                Models.Permission manageGallery = new Models.Permission
                {
                    ID = 26,
                    Name = "Manage gallery",
                    Feature = "Gallery",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    galleryCreate,
                    galleryEdit,
                    galleryDelete,
                    galleryEditImage
                }
                };

                Models.Permission viewCategory = new Models.Permission { ID = 27, Name = "View category", Feature = "Category", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { categoryIndex } };
                Models.Permission manageCategory = new Models.Permission
                {
                    ID = 28,
                    Name = "Manage category",
                    Feature = "Category",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    categoryCreate,
                    categoryEdit,
                    categoryDelete,
                    categoryEditImage
                }
                };

                Models.Permission viewUndeliveryReason = new Models.Permission { ID = 29, Name = "View undelivery reason", Feature = "Undelivery Reason", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { undeliveryReasonIndex } };
                Models.Permission manageUndeliveryReason = new Models.Permission
                {
                    ID = 30,
                    Name = "Manage undelivery reason",
                    Feature = "Undelivery Reason",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   undeliveryReasonCreate,
                   undeliveryReasonEdit,
                   undeliveryReasonDelete
                }
                };

                Models.Permission viewEmail = new Models.Permission { ID = 31, Name = "View email", Feature = "Email", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { emailIndex } };
                Models.Permission manageEmail = new Models.Permission
                {
                    ID = 32,
                    Name = "Manage email",
                    Feature = "Email",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   emailCreate,
                   emailEdit,
                   emailDelete
                }
                };

                Models.Permission viewPageManager = new Models.Permission { ID = 33, Name = "View page manager", Feature = "Page Manager", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { pageManagerIndex } };
                Models.Permission managePageManager = new Models.Permission
                {
                    ID = 34,
                    Name = "Manage page manager",
                    Feature = "Page Manager",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   pageManagerCreate,
                   pageManagerEdit,
                   pageManagerDelete
                }
                };

                Models.Permission viewStreet = new Models.Permission { ID = 35, Name = "View street", Feature = "Street", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { streetIndex } };
                Models.Permission manageStreet = new Models.Permission
                {
                    ID = 36,
                    Name = "Manage street",
                    Feature = "Street",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   streetCreate,
                   streetEdit,
                   streetDelete
                }
                };

                Models.Permission viewSubCategory = new Models.Permission { ID = 37, Name = "View sub category", Feature = "Sub Category", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { subCategoryIndex } };
                Models.Permission manageSubCategory = new Models.Permission
                {
                    ID = 38,
                    Name = "Manage sub category",
                    Feature = "Sub Category",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   subCategoryCreate,
                   subCategoryEdit,
                   subCategoryDelete
                }
                };

                Models.Permission viewStyleClasses = new Models.Permission { ID = 39, Name = "View style class", Feature = "Style Class", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { styleClassesIndex } };
                Models.Permission manageStyleClasses = new Models.Permission
                {
                    ID = 40,
                    Name = "Manage style class",
                    Feature = "Style Class",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   styleClassesCreate,
                   styleClassesEdit,
                   styleClassesDelete
                }
                };

                Models.Permission viewStyleTraits = new Models.Permission { ID = 41, Name = "View style traits", Feature = "Style Traits", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { styleTraitsIndex } };
                Models.Permission manageStyleTraits = new Models.Permission
                {
                    ID = 42,
                    Name = "Manage style traits",
                    Feature = "Style Traits",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   styleTraitsCreate,
                   styleTraitsEdit,
                   styleTraitsDelete
                }
                };

                Models.Permission viewStyleTraitValues = new Models.Permission { ID = 43, Name = "View style trait values", Feature = "Style Trait Values", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { styleTraitValuesIndex } };
                Models.Permission manageStyleTraitValues = new Models.Permission
                {
                    ID = 44,
                    Name = "Manage style trait values",
                    Feature = "Style Trait Values",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   styleTraitValuesCreate,
                   styleTraitValuesEdit,
                   styleTraitValuesDelete
                }
                };

                Models.Permission viewUOM = new Models.Permission { ID = 45, Name = "View unit of measure", Feature = "Unit of Measure", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { uomIndex } };
                Models.Permission manageUOM = new Models.Permission
                {
                    ID = 46,
                    Name = "Manage unit of measure",
                    Feature = "Unit of Measure",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                   uomCreate,
                   uomEdit,
                   uomDelete
                }
                };

                Models.Permission viewProduct = new Models.Permission { ID = 47, Name = "View product", Feature = "Product", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { productIndex } };
                Models.Permission manageProduct = new Models.Permission
                {
                    ID = 48,
                    Name = "Manage product",
                    Feature = "Product",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                      productManage,
                      productCategory,
                      productDiscount,
                      productImages,
                      productProductVariant,
                      productDDStyleTraitByStyleClassId,
                      productDDStyleTraitValueByStyleTraitId,
                      productDDSubcategoryByCategoryId
                }
                };

                Models.Permission viewCoupon = new Models.Permission { ID = 49, Name = "View coupon", Feature = "Coupon", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { couponIndex } };
                Models.Permission manageCoupon = new Models.Permission
                {
                    ID = 50,
                    Name = "Manage coupon",
                    Feature = "Coupon",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                      couponCreate,
                      couponEdit,
                      couponDelete
                }
                };

                Models.Permission viewStore = new Models.Permission { ID = 51, Name = "View store", Feature = "Store", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { storeIndex } };
                Models.Permission manageStore = new Models.Permission
                {
                    ID = 52,
                    Name = "Manage store",
                    Feature = "Store",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                      storeCreate,
                      storeEdit,
                      storeDelete
                }
                };

                Models.Permission viewPushNotification = new Models.Permission { ID = 53, Name = "View push notification", Feature = "PushNotification", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { pushNotificationIndex } };
                Models.Permission managePushNotification = new Models.Permission
                {
                    ID = 54,
                    Name = "Manage push notification",
                    Feature = "PushNotification",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                     pushNotificationCreate,
                     pushNotificationReminder
                }
                };

                Models.Permission viewTimeSlot = new Models.Permission { ID = 55, Name = "View timeslot", Feature = "TimeSlot", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { timeSlotIndex } };
                Models.Permission manageTimeSlot = new Models.Permission
                {
                    ID = 56,
                    Name = "Manage timeslot",
                    Feature = "TimeSlot",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                     timeSlotCreate,
                     timeSlotEdit
                }
                };


                Models.Permission viewOrders = new Models.Permission { ID = 57, Name = "View orders", Feature = "Orders", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { orderIndex, orderDetails, orderInstaService, orderScheduleService, orderCompleted, orderAssigned, orderCancelled, orderOneTouch, orderWhatsApp, orderCustomize, orderPackage, } };
                Models.Permission manageOrders = new Models.Permission
                {
                    ID = 58,
                    Name = "Manage orders",
                    Feature = "Orders",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    orderAssign,
                    orderMarkAsDelivered,
                    orderExport,
                    orderCancel,
                    orderSendEmail,
                    orderOrderListItem,
                    orderMarkAsPickedUp,
                    orderAssignToVendor,
                    orderPickupFromVendor,
                    orderAssignForDelviery,
                    orderMarkDelivered
                }
                };

                Models.Permission viewCustomer = new Models.Permission { ID = 59, Name = "View customer", Feature = "Customer", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { customerIndex } };
                Models.Permission manageCustomer = new Models.Permission
                {
                    ID = 60,
                    Name = "Manage customer",
                    Feature = "Customer",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    customerAssignCredits,
                    customerAddresses,
                    customerAddressCreate,
                    customerMapAddress
                }
                };

                Models.Permission viewReturnCan = new Models.Permission { ID = 61, Name = "View return can", Feature = "ReturnCan", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { returnCanIndex, returnCanDetails, returnCanCompleted, returnCanAssigned } };
                Models.Permission manageReturnCan = new Models.Permission
                {
                    ID = 62,
                    Name = "Manage return can",
                    Feature = "ReturnCan",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                     returnCanAssignToPickUp
                }
                };

                Models.Permission viewDelivery = new Models.Permission { ID = 63, Name = "View delviery", Feature = "Delviery", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { deliveryIndex, deliveryCompletedDelivery, deliveryDetails, deliveryAssignedReturnCan, deliveryCompletedReturncan, deliveryReturnCanDetails } };
                Models.Permission manageDelivery = new Models.Permission
                {
                    ID = 64,
                    Name = "Manage delivery",
                    Feature = "Delviery",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                     deliveryMarkAsDelivered,
                     deliveryMarkedAsPickUp
                }
                };


                Models.Permission viewAppMessage = new Models.Permission { ID = 65, Name = "View app message", Feature = "AppMessage", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { appMessageIndex } };
                Models.Permission manageAppMessage = new Models.Permission
                {
                    ID = 66,
                    Name = "Manage app message",
                    Feature = "AppMessage",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                     appMessageCreate,
                     appMessageEdit,
                     appMessageDelete
                }
                };

                Models.Permission viewDeliveryBoy = new Models.Permission { ID = 67, Name = "View delivery boy", Feature = "DeliveryBoy", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { deliveryBoyIndex } };
                Models.Permission manageDeliveryBoy = new Models.Permission
                {
                    ID = 68,
                    Name = "Manage delivery boy",
                    Feature = "DeliveryBoy",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                     deliveryBoyCreate,
                     deliveryBoyEdit,
                     deliveryBoyDelete
                }
                };

                Models.Permission viewVendor = new Models.Permission { ID = 69, Name = "View vendor", Feature = "Vendor", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { vendorIndex } };
                Models.Permission manageVendor = new Models.Permission
                {
                    ID = 70,
                    Name = "Manage vendor",
                    Feature = "Vendor",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    vendorCreate,
                    vendorEdit
                }
                };

                Models.Permission viewServiceType = new Models.Permission { ID = 71, Name = "View service type", Feature = "ServiceType", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { ServiceTypeIndex } };
                Models.Permission manageServiceType = new Models.Permission
                {
                    ID = 72,
                    Name = "Manage service type",
                    Feature = "ServiceType",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    ServiceTypeCreate,
                    ServiceTypeEdit,
                    serviceTypeDelete
                }
                };

                Models.Permission viewCloths = new Models.Permission { ID = 73, Name = "View cloths", Feature = "Cloths", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { clothsIndex } };
                Models.Permission manageCloths = new Models.Permission
                {
                    ID = 74,
                    Name = "Manage cloths",
                    Feature = "Cloths",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    clothsCreate,
                    clothsEdit,
                    clothsDelete
                }
                };

                Models.Permission viewRateCard = new Models.Permission { ID = 75, Name = "View rate card", Feature = "RateCard", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { rateCardIndex } };
                Models.Permission manageRateCard = new Models.Permission
                {
                    ID = 76,
                    Name = "Manage rate card",
                    Feature = "RateCard",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    rateCardCreate,
                    rateCardEdit,
                    rateCardDelete
                }
                };

                Models.Permission viewPackage = new Models.Permission { ID = 77, Name = "View package", Feature = "Package", CreatedOn = DateTime.Now, Actions = new List<Models.Action>() { packageIndex } };
                Models.Permission managePackage = new Models.Permission
                {
                    ID = 78,
                    Name = "Manage package",
                    Feature = "Package",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    packageCreate,
                    packageEdit,
                    packageDelete
                }
                };

                Models.Permission manageReports = new Models.Permission
                {
                    ID = 79,
                    Name = "Manage Reports",
                    Feature = "Reports",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    reportsOrderReport,
                    reportsProfitAndLoss
                }
                };

                Models.Permission manageHomeScreenLayout = new Models.Permission
                {
                    ID = 80,
                    Name = "Manage HomeScreen Layout",
                    Feature = "HomeScreenLayout",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                    homeScreenLayoutIndex,
                    homeScreenLayoutEdit
                }
                };

                Models.Permission manageClassifiedLayout = new Models.Permission
                {
                    ID = 81,
                    Name = "Manage Classified Layout",
                    Feature = "Classified",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                        classifiedIndex,
                        classifiedAdd,
                        classifiedListIndex,
                        classifiedListEdit,
                        classifiedListDelete
                    }
                };

                Models.Permission manageCarouselLayout = new Models.Permission
                {
                    ID = 82,
                    Name = "Manage Carousel",
                    Feature = "Carousel",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                        carouselIndex,
                        carouselCreate,
                        carouselEdit,
                        carouselDelete
                    }
                };

                Models.Permission manageClassifiedArticle = new Models.Permission
                {
                    ID = 83,
                    Name = "Manage Classified Arcticle",
                    Feature = "Classified Arcticle",
                    CreatedOn = DateTime.Now,
                    Actions = new List<Models.Action>() {
                        classifiedArticleIndex,
                        classifiedArticleCreate,
                        classifiedArticleEdit,
                        classifiedArticleDelete
                    }
                };

                context.Permission.AddOrUpdate(p => p.ID,
                    viewHome,

                    viewRole,
                    manageRole,

                    viewUser,
                    manageUser,

                    viewMenu,
                    manageMenu,

                    viewAction,
                    manageAction,

                    viewPermission,
                    managePermission,

                    manageClearCache,

                    viewCompany,
                    manageCompany,

                    viewSupportFAQ,
                    manageSupportFAQ,

                    viewSystemSettings,
                    manageSystemSettings,

                    viewCity,
                    manageCity,

                    viewArea,
                    manageArea,

                    viewImageCategory,
                    manageImageCategory,

                    viewGallery,
                    manageGallery,

                    viewCategory,
                    manageCategory,

                    viewUndeliveryReason,
                    manageUndeliveryReason,

                    viewEmail,
                    manageEmail,

                    viewPageManager,
                    managePageManager,

                    viewStreet,
                    manageStreet,

                    viewSubCategory,
                    manageSubCategory,

                    viewStyleClasses,
                    manageStyleClasses,

                    viewStyleTraits,
                    manageStyleTraits,

                    viewStyleTraitValues,
                    manageStyleTraitValues,

                    viewUOM,
                    manageUOM,

                    viewProduct,
                    manageProduct,

                    viewCoupon,
                    manageCoupon,

                    viewStore,
                    manageStore,

                    viewPushNotification,
                    managePushNotification,

                    viewTimeSlot,
                    manageTimeSlot,

                    viewAppMessage,
                    manageAppMessage,

                    viewDeliveryBoy,
                    manageDeliveryBoy,

                    viewOrders,
                    manageOrders,

                    viewCustomer,
                    manageCustomer,

                    viewReturnCan,
                    manageReturnCan,

                    viewDelivery,
                    manageDelivery,

                    viewVendor,
                    manageVendor,

                    viewServiceType,
                    manageServiceType,

                    viewCloths,
                    manageCloths,

                    viewRateCard,
                    manageRateCard,

                    viewPackage,
                    managePackage,

                    manageReports,

                    manageHomeScreenLayout,

                    manageClassifiedLayout,

                    manageCarouselLayout,

                    manageClassifiedArticle
                   );

                Models.Role roleSuperAdministrator = new Models.Role
                {
                    ID = 1,
                    Name = "Super Administrator",
                    DefaultController = "Home",
                    DefaultAction = "Index",
                    CreatedOn = DateTime.Now,
                    Permissions = new List<Models.Permission>() {
                viewRole,
                viewHome,
                viewUser,
                manageUser,
                manageRole,
                viewMenu,
                manageMenu,
                viewAction,
                manageAction,
                viewPermission,
                managePermission,
                manageClearCache,
                viewCompany,
                manageCompany,
                viewSupportFAQ,
                manageSupportFAQ,
                viewSystemSettings,
                manageSystemSettings,
                viewCity,
                manageCity,
                viewArea,
                manageArea,
                viewImageCategory,
                manageImageCategory,
                viewGallery,
                manageGallery,
                viewCategory,
                manageCategory,
                viewUndeliveryReason,
                manageUndeliveryReason,
                viewEmail,
                manageEmail,
                viewPageManager,
                managePageManager,
                viewStreet,
                manageStreet,
                viewSubCategory,
                manageSubCategory,
                viewStyleClasses,
                manageStyleClasses,
                viewStyleTraits,
                manageStyleTraits,
                viewStyleTraitValues,
                manageStyleTraitValues,
                viewUOM,
                manageUOM,
                viewProduct,
                manageProduct,
                viewCoupon,
                manageCoupon,
                viewStore,
                manageStore,
                viewPushNotification,
                managePushNotification,
                viewTimeSlot,
                manageTimeSlot,
                viewAppMessage,
                manageAppMessage,
                viewDeliveryBoy,
                manageDeliveryBoy,
                viewOrders,
                manageOrders,
                viewCustomer,
                manageCustomer,
                viewReturnCan,
                manageReturnCan,
                viewDelivery,
                manageDelivery,
                viewVendor,
                manageVendor,
                viewServiceType,
                manageServiceType,
                viewCloths,
                manageCloths,
                viewRateCard,
                manageRateCard,
                viewPackage,
                managePackage,
                manageReports,
                manageHomeScreenLayout,
                manageClassifiedLayout,
                manageCarouselLayout,
                manageClassifiedArticle
                }
                };

                context.Role.AddOrUpdate(
                  r => r.ID,
                  roleSuperAdministrator
                  );

                context.SystemSettings.AddOrUpdate(
                    x => x.ID,
                    new Models.SystemSetting { ID = 1, SettingType = Models.SettingType.EmailTemplate, Name = "Email template", Comments = "Email Template", Value = "<!DOCTYPE html><html><head><title></title><meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"><meta name=\"viewport\" content=\"width=device-width,initial-scale=1\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><style type=\"text/css\">/ FONTS /@import url('https://fonts.googleapis.com/css?family=Poppins:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i');/ CLIENT-SPECIFIC STYLES /body, table, td, a {-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;}table, td {mso-table-lspace: 0pt;mso-table-rspace: 0pt;}img {-ms-interpolation-mode: bicubic;}/ RESET STYLES /img {border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;}table {border-collapse: collapse !important;}body {height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;}/ iOS BLUE LINKS /a[x-apple-data-detectors] {color: inherit !important;text-decoration: none !important;font-size: inherit !important;font-family: inherit !important;font-weight: inherit !important;line-height: inherit !important;}/ MOBILE STYLES /@media screen and (max-width:600px) {h1 {font-size: 32px !important;line-height: 32px !important;}}/ ANDROID CENTER FIX /div[style*=\"margin:16px 0;\"] {margin: 0 !important;}</style></head><body style=\"background-color:#f3f5f7;margin:0!important;padding:0!important;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><!-- LOGO --><tr><td align=\"center\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"><tr><td align=\"center\" valign=\"top\" width=\"600\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding:40px 10px 40px 10px;\"><a href=\"#\" target=\"_blank\" style=\"text-decoration:none;\"><span style=\"display:block;font-family:'Poppins',sans-serif;color:#333333;font-size: 36px;\" border=\"0\"><b>Risible.in</b></span></a></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><!-- HERO --><tr><td align=\"center\" style=\"padding:0px 10px 0px 10px;\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"><tr><td align=\"center\" valign=\"top\" width=\"600\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding:40px 20px 20px 20px;border-radius:4px 4px 0px 0px;color:#111111;font-family:'Poppins',sans-serif;font-size:48px;font-weight:400;letter-spacing:2px;line-height:48px;\"><h1 style=\"font-size:42px;font-weight:400;margin:0;\">{{heading}}</h1></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><!-- COPY BLOCK --><tr><td align=\"center\" style=\"padding:0px 10px 0px 10px;\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"><tr><td align=\"center\" valign=\"top\" width=\"600\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;\">{{body}}<!-- COPY --><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding:0px 30px 40px 30px;border-radius:0px 0px 0px 0px;color:#666666;font-family:'Poppins',sans-serif;font-size:14px;font-weight:400;line-height: 25px;\"><p style=\"margin:0;\">Regards,<br>Risible.in</p></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr><!-- SUPPORT CALLOUT --><tr><td align=\"center\" style=\"padding:10px 10px 0px 10px;\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"><tr><td align=\"center\" valign=\"top\" width=\"600\"><![endif]--><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;\"><!-- HEADLINE --><tr><td bgcolor=\"#398bf7\" align=\"center\" style=\"padding:30px 30px 30px 30px;border-radius:4px 4px 4px 4px;color:#666666;font-family:'Poppins',sans-serif;font-size:16px;font-weight:400;line-height:25px;\"><h2 style=\"font-size:16px;font-weight:400;color:#ffffff;margin:0;\">Need more help?</h2><p style=\"margin:0;font-size:14px;\"><a href=\"#\" target=\"_blank\" style=\"color:#ffffff;\">We&rsquo;re here, ready to talk</a></p></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></table></body></html>" },
                    new Models.SystemSetting { ID = 2, SettingType = Models.SettingType.EmailBodyResetPassword, Name = "Email Body Reset Password", Comments = "Email Body Reset Password", Value = "<!-- COPY --><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding:20px 30px 40px 30px;color:#666666;font-family:'Poppins',sans-serif;font-size:16px;font-weight:400;line-height:25px;\"><p style=\"margin:0;\">There is a request to change your password. Resetting your password is easy. Just press the button below and follow the instructions. We'll have you up and running in no time.</p></td></tr><!-- BULLETPROOF BUTTON --><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding:20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" style=\"border-radius:3px;\"bgcolor=\"#398bf7\"><a href=\"{{url}}\" target=\"_blank\" style=\"font-size:18px;font-family:Helvetica,Arial,sans-serif;color:#ffffff;text-decoration:none;color:#ffffff;text-decoration:none;padding:12px 50px;border-radius:2px;border:1px solid #398bf7;display:inline-block;\">Reset Password</a></td></tr></table></td></tr></table></td></tr><!-- COPY --><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding:0px 30px 20px 30px;color:#aaaaaa;font-family:&apos;Lato&apos;,Helvetica,Arial,sans-serif;font-size:13px;font-weight:400;line-height:25px;\"><p style=\"margin:0;text-align:center;\">If you did not make this request, just ignore this email. Otherwise, pleas click button above to change your password.</p></td></tr><!-- COPY -->" },
                    new Models.SystemSetting { ID = 3, SettingType = Models.SettingType.EmailCancelOrderCustomer, Name = "Email Body Cancel Order Customer", Comments = "Email Body Cancel Order Customer", Value = "<tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding:20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td>Dear {{name}},<br/><br/>Based on your request, your order {{systemOrderId}} for the below listed item has been cancelled by the seller.<br/><br/>Product Name - {{productName}} <br/><br/> Qantity - {{quantity}} <br/><br/> Rate  ₹ {{amount}}.</td></tr></table></td></tr></table></td></tr>" },
                    new Models.SystemSetting { ID = 4, SettingType = Models.SettingType.EmailCancelOrderAdmin, Name = "Email Body Cancel Order Admin", Comments = "Email Body Cancel Order Admin", Value = "<tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding:20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td>Dear {{name}},<br/><br/>{{customerName}}, has requested for order cancelation for orderId {{systemOrderId}} for the below listed item.<br/><br/>Product Name - {{productName}} <br/><br/> Qantity - {{quantity}} <br/><br/> Rate  ₹ {{amount}}.</td></tr></table></td></tr></table></td></tr>" },
                    new Models.SystemSetting { ID = 5, SettingType = Models.SettingType.EmailAdminContact, Name = "Email Body Customer Enquiry Admin", Comments = "Email Body Customer Enquiry Admin", Value = "<tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding:20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td>Dear {{name}},<br/><br/>You have received one enquiry from your website.<br/><br/>The Customer details are as below: <br/><br/> Name - {{CustomerName}} <br/><br/> Email - {{CustomerEmail}}<br/><br/> Message For You: - {{Message}}.<br/><br/><td></tr></table></td></tr></table></td></tr>" },
                    new Models.SystemSetting { ID = 6, SettingType = Models.SettingType.EmailNewUserAdded, Name = "Email Body new user created", Comments = "Email Body to send credentials to new user", Value = "<tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding:20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td>Dear {{name}},<br/><br/>Welcome to delivery management system.<br/><br/>Please find login credentials below: <br/><br/> Username - {{username}} <br/><br/> password - {{password}}<br/><br/><b>Note : Kindly change autogenerated password once login successfully</b><br/></br><a href=\"{{ appurl }}\" taget=\"_blank\">Click Here To Login</a><td></tr></table></td></tr></table></td></tr>" },
                    new Models.SystemSetting { ID = 7, SettingType = Models.SettingType.GridColumns, Name = "Number of Columns on Home Screen", Comments = "Number of columns on Home Screen to render Category", Value = "4" },
                    new Models.SystemSetting { ID = 8, SettingType = Models.SettingType.OutOfRating, Name = "The out of rating for classified.", Comments = "The out of rating for classified.", Value = "5" },
                    new Models.SystemSetting { ID = 9, SettingType = Models.SettingType.AllowedImageExtensions, Name = "List of allowed image extensions.", Comments = "Semicolon seperated list of allowed extensions", Value = ".jpg;.jpeg;.png" },
                    new Models.SystemSetting { ID = 10, SettingType = Models.SettingType.AdminEmails, Name = "List of email addresses for Best Deal.", Comments = "Pipe (|) seperated list of email ids to keep in CC for Best deal email.", Value = "abc@yopmail.com|xyz@yopmail.com" },
                    new Models.SystemSetting { ID = 11, SettingType = Models.SettingType.TesterNumberList, Name = "Tester Number List", Comments = "Tester Number List.", Value = "9222360780,8668821214,8793592252" },
                    new Models.SystemSetting { ID = 12, SettingType = Models.SettingType.PushNotificationServerKey, Name = "Server Key", Comments = "Server Key.", Value = "AAAABpCM5J4:APA91bEki3Fp_V1FF5N8yhFD0nzKYA5EOiOFoXLoetHxiM9-Tu2SAtA4hQfqHgSIitiEsCzkBlZDn5jjyDhiC6c7Vco2oIWC5qPuzIAt4zEkUcO57EPn_VxsExOgK_JkPDGGbdAzFD5v" },
                    new Models.SystemSetting { ID = 13, SettingType = Models.SettingType.PushNotificationServerId, Name = "Server Id", Comments = "Server Id.", Value = "28194956446" },
                    new Models.SystemSetting { ID = 14, SettingType = Models.SettingType.PushNotificationGroupCreateURL, Name = "FCM Group Url", Comments = "FCM Group Url", Value = "https://fcm.googleapis.com/fcm/notification" },
                    new Models.SystemSetting { ID = 15, SettingType = Models.SettingType.PushNotifcationURL, Name = "FCM Url", Comments = "FCM Url", Value = "https://fcm.googleapis.com/fcm/send" },
                    new Models.SystemSetting { ID = 16, SettingType = Models.SettingType.MinimumSupportedVersion, Name = "Minimum Supported", Comments = "Minimum Supported", Value = "1.0.0" }
                    );

                context.AppMessages.AddOrUpdate(
                    x => x.ID,
                    new Models.AppMessage { ID = 1, Key = "currency", Message = "AED", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 2, Key = "package_msg", Message = "Customizable message for package", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 3, Key = "email_corporate", Message = "sales@e-laundry.in", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 4, Key = "email_contact", Message = "contact@e-laundry.in", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 5, Key = "phone_corporate", Message = "+971 123456789", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 6, Key = "phone_contact", Message = "+971 987654321", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 7, Key = "text_contact", Message = "Text", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 8, Key = "whatsapp", Message = "+971 987654321", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 9, Key = "phone_text_contact", Message = "Get in touch with us via phone number given below.", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 10, Key = "email_text_contact", Message = "Get in touch with us through mail", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 11, Key = "tagline", Message = "Leave The Dirt On Us", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 12, Key = "additional_message", Message = "Test Additional Message", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 13, Key = "terms", Message = "http://test.e-laundry.in/TNC", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 14, Key = "privacy", Message = "http://test.e-laundry.in/Privacy", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 15, Key = "whatsapp_number", Message = "+971585806500", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now },
                    new Models.AppMessage { ID = 16, Key = "availablityMessage", Message = "Service Not available in your current location, please select another address or try again later", Status = true, CreatedBy = "Seed", CreatedDate = DateTime.Now }
                    );

                context.Menus.AddOrUpdate(
              p => p.ID,
              new Models.Menu { ID = 1, Name = "Security", HideFromSystemAdministrator = true, DisplayName = "Security", Order = 1, ActionName = null, Controller = null, CreatedDate = DateTime.UtcNow, Icon = "fa fa-shield", CreatedBy = "System" },

              new Models.Menu { ID = 2, Name = "Role", DisplayName = "Roles", Order = 1, ActionName = "Index", Controller = "RoleMaster", ParentId = 1, CreatedDate = DateTime.UtcNow, Icon = "fa fa-user-secret", CreatedBy = "System" },
              new Models.Menu { ID = 3, Name = "User", DisplayName = "Users", Order = 2, ActionName = "Index", Controller = "User", ParentId = 1, CreatedDate = DateTime.UtcNow, Icon = "fa fa-user", CreatedBy = "System" },
              new Models.Menu { ID = 4, Name = "Action", DisplayName = "Action", Order = 3, ActionName = "Index", Controller = "Action", ParentId = 1, CreatedDate = DateTime.UtcNow, Icon = "fa fa-shield", CreatedBy = "System" },
              new Models.Menu { ID = 5, Name = "Permission", DisplayName = "Permission", Order = 4, ActionName = "Index", Controller = "Permission", ParentId = 1, CreatedDate = DateTime.UtcNow, Icon = "fa fa-key", CreatedBy = "System" },
              new Models.Menu { ID = 6, Name = "Menu", DisplayName = "Menu", Order = 5, ActionName = "Index", Controller = "Menu", ParentId = 1, CreatedDate = DateTime.UtcNow, Icon = "fa fa-list-alt", CreatedBy = "System" },
              new Models.Menu { ID = 7, Name = "Memory cache", DisplayName = "Memory cache", Order = 6, ActionName = "Clear", Controller = "ClearCache", ParentId = 1, CreatedDate = DateTime.UtcNow, Icon = "fa fa-refresh", CreatedBy = "System" },

              new Models.Menu { ID = 8, Name = "Master Management", DisplayName = "Master Management", Order = 2, ActionName = null, Controller = null, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },

              new Models.Menu { ID = 9, Name = "DeliveryBoy", DisplayName = "DeliveryBoy", Order = 1, ActionName = "Index", Controller = "DeliveryBoy", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },
              new Models.Menu { ID = 10, Name = "Vendor", DisplayName = "Vendor", Order = 2, ActionName = "Index", Controller = "Vendor", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },
              new Models.Menu { ID = 11, Name = "Category", DisplayName = "Category", Order = 3, ActionName = "Index", Controller = "Category", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },
              new Models.Menu { ID = 12, Name = "Subcategory", DisplayName = "Subcategory", Order = 4, ActionName = "Index", Controller = "Subcategory", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },
              new Models.Menu { ID = 13, Name = "ServiceType", DisplayName = "ServiceType", Order = 5, ActionName = "Index", Controller = "ServiceType", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },
              new Models.Menu { ID = 14, Name = "Cloths", DisplayName = "Cloths", Order = 6, ActionName = "Index", Controller = "Cloths", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },
              new Models.Menu { ID = 15, Name = "Rate Card", DisplayName = "Rate Card", Order = 7, ActionName = "Index", Controller = "RateCard", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },
              new Models.Menu { ID = 16, Name = "Package Master", DisplayName = "Package Master", Order = 8, ActionName = "Index", Controller = "Package Master", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },
              new Models.Menu { ID = 17, Name = "Coupon", DisplayName = "Coupon", Order = 9, ActionName = "Index", Controller = "Coupon", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-percent", CreatedBy = "System" },
              new Models.Menu { ID = 18, Name = "SupportAndFAQ", DisplayName = "SupportAndFAQ", Order = 10, ActionName = "Index", Controller = "SupportAndFAQ", ParentId = 8, CreatedDate = DateTime.UtcNow, Icon = "fa fa-question-circle", CreatedBy = "System" },

              new Models.Menu { ID = 19, Name = "Order Management", DisplayName = "Order Management", Order = 3, ActionName = null, Controller = null, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },

              new Models.Menu { ID = 20, Name = "All Orders", DisplayName = "All Orders", Order = 1, ActionName = "Index", Controller = "Orders", ParentId = 19, CreatedDate = DateTime.UtcNow, Icon = "fa fa-shopping-basket", CreatedBy = "System" },
              new Models.Menu { ID = 21, Name = "WhatsApp", DisplayName = "WhatsApp", Order = 2, ActionName = "WhatsApp", Controller = "Orders", ParentId = 19, CreatedDate = DateTime.UtcNow, Icon = "fa fa-whatsapp", CreatedBy = "System" },
              new Models.Menu { ID = 22, Name = "OneTouch", DisplayName = "OneTouch", Order = 3, ActionName = "OneTouch", Controller = "Orders", ParentId = 19, CreatedDate = DateTime.UtcNow, Icon = "fa fa-hand-o-up", CreatedBy = "System" },
              new Models.Menu { ID = 23, Name = "Customize", DisplayName = "Customize", Order = 4, ActionName = "Customize", Controller = "Orders", ParentId = 19, CreatedDate = DateTime.UtcNow, Icon = "fa fa-pencil-square-o", CreatedBy = "System" },
              new Models.Menu { ID = 24, Name = "Package", DisplayName = "Package", Order = 5, ActionName = "Package", Controller = "Orders", ParentId = 19, CreatedDate = DateTime.UtcNow, Icon = "fa fa-shopping-bag", CreatedBy = "System" },

              new Models.Menu { ID = 25, Name = "PushNotification", DisplayName = "PushNotification", Order = 4, ActionName = "Index", Controller = "PushNotification", CreatedDate = DateTime.UtcNow, Icon = "fa fa-comments", CreatedBy = "System" },

              new Models.Menu { ID = 26, Name = "PushNotification Reminder", DisplayName = "Reminder", Order = 5, ActionName = "Reminder", Controller = "PushNotification", CreatedDate = DateTime.UtcNow, Icon = "fa fa-bell", CreatedBy = "System" },

              new Models.Menu { ID = 27, Name = "Configuration", DisplayName = "Configuration", Order = 6, ActionName = null, Controller = null, CreatedDate = DateTime.UtcNow, Icon = "fa fa-cog", CreatedBy = "System" },

              new Models.Menu { ID = 28, Name = "AppMessage", DisplayName = "AppMessage", Order = 1, ActionName = "Index", Controller = "AppMessage", ParentId = 27, CreatedDate = DateTime.UtcNow, Icon = "fa fa-commenting", CreatedBy = "System" },
              new Models.Menu { ID = 29, Name = "SystemSetting", DisplayName = "System Setting", Order = 2, ActionName = "Index", Controller = "SystemSetting", ParentId = 27, CreatedDate = DateTime.UtcNow, Icon = "fa fa-wrench", CreatedBy = "System" },
              new Models.Menu { ID = 30, Name = "HomeScreen Layout", DisplayName = "HomeScreen Layout", Order = 3, ActionName = "Index", Controller = "HomeScreenLayout", ParentId = 27, CreatedDate = DateTime.UtcNow, Icon = "fa fa-home", CreatedBy = "System" },

              // Customer
              new Models.Menu { ID = 31, Name = "Customer", DisplayName = "Customer", Order = 7, ActionName = "Index", Controller = "Customer", CreatedDate = DateTime.UtcNow, Icon = "fa fa-user", CreatedBy = "System" },

              // Reports
              new Models.Menu { ID = 32, Name = "Reports", DisplayName = "Reports", Order = 8, ActionName = null, Controller = null, CreatedDate = DateTime.UtcNow, Icon = "fa fa-file-excel-o", CreatedBy = "System" },

              new Models.Menu { ID = 33, Name = "Order Report", DisplayName = "Order Report", Order = 1, ActionName = "OrderReport", Controller = "Reports", ParentId = 32, CreatedDate = DateTime.UtcNow, Icon = "fa fa-cart-arrow-down", CreatedBy = "System" },
              new Models.Menu { ID = 34, Name = "Profit and Loss", DisplayName = "Profit and Loss", Order = 2, ActionName = "ProfitAndLoss", Controller = "Reports", ParentId = 32, CreatedDate = DateTime.UtcNow, Icon = "fa fa-usd", CreatedBy = "System" },

              new Models.Menu { ID = 35, Name = "Classified", DisplayName = "Classified", Order = 9, ActionName = null, Controller = null, CreatedDate = DateTime.UtcNow, Icon = "fa fa-database", CreatedBy = "System" },

              new Models.Menu { ID = 36, Name = "Add New", DisplayName = "Add New", Order = 1, ActionName = "Index", Controller = "AddNewClassified", ParentId = 35, CreatedDate = DateTime.UtcNow, Icon = "fa fa-plus-circle", CreatedBy = "System" },
              new Models.Menu { ID = 37, Name = "List", DisplayName = "List", Order = 2, ActionName = "Index", Controller = "ClassifiedList", ParentId = 35, CreatedDate = DateTime.UtcNow, Icon = "fa fa-list-alt", CreatedBy = "System" },
              new Models.Menu { ID = 38, Name = "Article", DisplayName = "Article", Order = 3, ActionName = "Index", Controller = "ClassifiedArticle", ParentId = 35, CreatedDate = DateTime.UtcNow, Icon = "fa fa-newspaper-o", CreatedBy = "System" },

              new Models.Menu { ID = 39, Name = "Carousel", DisplayName = "Carousel", Order = 10, ActionName = "Index", Controller = "Carousel", CreatedDate = DateTime.UtcNow, Icon = "fa fa-sliders", CreatedBy = "System" }
              );

                base.Seed(context);
            }

        }
    }
}
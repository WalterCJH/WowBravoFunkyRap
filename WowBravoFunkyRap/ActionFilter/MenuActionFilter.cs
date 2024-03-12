using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WowBravoFunkyRap.Areas.Manage.ViewModels.Layout;
using WowBravoFunkyRap.Model.Enums;
using WowBravoFunkyRap.Shared.Services.Interface;

namespace WowBravoFunkyRap.ActionFilter
{
    public class MenuActionFilter : IActionFilter
    {
        private readonly IClaimService _claimService;

        public MenuActionFilter(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public struct RoleInfo
        {
            public string MenuTitle;
            public string MenuIcon;
            public int MenuSequence;
            public string MenuItemTitle;
            public string MenuItemController;
            public string MenuItemAction;
            public int MenuItemSequence;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                var claimRoles = _claimService.QueryUserRole();
                if (claimRoles.Count() > 0)
                {
                    var menus = new List<Menu>();

                    string usualSettingTitle = "通用設定";
                    string usualSettingIconPath = "fas fa-stream";
                    int usualSettingSeq = 1;

                    string reportTitle = "報表";
                    string reportIconPath = "/coreui/vendors/@coreui/icons/svg/free.svg#cil-file";
                    int reportSeq = 2;

                    string userSettingTitle = "使用者設定";
                    string userSettingIconPath = "fas fa-users";
                    int userSettingSeq = 3;

                    int sequence = 1;
                    var roleMapping = new Dictionary<string, RoleInfo>
                    {
                        { eRole.CityRead.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "鄉鎮市區",
                            MenuItemController = "Cities",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence
                        }},
                        { eRole.CityWrite.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "鄉鎮市區",
                            MenuItemController = "Cities",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence++
                        }},
                        { eRole.OrderCancelReasonRead.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "訂單取消原因",
                            MenuItemController = "OrderCancelReasons",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence
                        }},
                        { eRole.OrderCancelReasonWrite.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "訂單取消原因",
                            MenuItemController = "OrderCancelReasons",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence++
                        }},
                        { eRole.ShipMethodRead.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "配送方式",
                            MenuItemController = "ShipMethods",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence
                        }},
                        { eRole.ShipMethodWrite.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "配送方式",
                            MenuItemController = "ShipMethods",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence++
                        }},
                        { eRole.ProductCategoryRead.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "產品類別",
                            MenuItemController = "ProductCategories",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence
                        }},
                        { eRole.ProductCategoryWrite.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "產品類別",
                            MenuItemController = "ProductCategories",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence++
                        }},
                        { eRole.PublicityImageRead.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "宣傳圖片",
                            MenuItemController = "PublicityImages",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence
                        }},
                        { eRole.PublicityImageWrite.ToString(), new RoleInfo {
                            MenuTitle = usualSettingTitle,
                            MenuIcon = usualSettingIconPath,
                            MenuSequence = usualSettingSeq,
                            MenuItemTitle = "宣傳圖片",
                            MenuItemController = "PublicityImages",
                            MenuItemAction = "Index",
                            MenuItemSequence = sequence++
                        }},
                    };

                    if (_claimService.GetUserIsAdmin())
                    {
                        sequence = 1;
                        roleMapping.Add(
                            eRole.RoleRead.ToString(), new RoleInfo
                            {
                                MenuTitle = userSettingTitle,
                                MenuIcon = userSettingIconPath,
                                MenuSequence = userSettingSeq,
                                MenuItemTitle = "角色",
                                MenuItemController = "Roles",
                                MenuItemAction = "Index",
                                MenuItemSequence = sequence
                            });
                        roleMapping.Add(
                            eRole.RoleWrite.ToString(), new RoleInfo
                            {
                                MenuTitle = userSettingTitle,
                                MenuIcon = userSettingIconPath,
                                MenuSequence = userSettingSeq,
                                MenuItemTitle = "角色",
                                MenuItemController = "Roles",
                                MenuItemAction = "Index",
                                MenuItemSequence = sequence++
                            });
                        roleMapping.Add(
                            eRole.UserRead.ToString(), new RoleInfo
                            {
                                MenuTitle = userSettingTitle,
                                MenuIcon = userSettingIconPath,
                                MenuSequence = userSettingSeq,
                                MenuItemTitle = "使用者",
                                MenuItemController = "Users",
                                MenuItemAction = "Index",
                                MenuItemSequence = sequence
                            });
                        roleMapping.Add(
                            eRole.UserWrite.ToString(), new RoleInfo
                            {
                                MenuTitle = userSettingTitle,
                                MenuIcon = userSettingIconPath,
                                MenuSequence = userSettingSeq,
                                MenuItemTitle = "使用者",
                                MenuItemController = "Users",
                                MenuItemAction = "Index",
                                MenuItemSequence = sequence++
                            });
                    }

                    foreach (var roleStr in claimRoles)
                    {
                        if (roleMapping.TryGetValue(roleStr, out var roleInfo))
                        {
                            var menu = menus.FirstOrDefault(c => c.Title == roleInfo.MenuTitle);
                            if (menu == null)
                            {
                                menu = new Menu { Title = roleInfo.MenuTitle, IconClass = roleInfo.MenuIcon, Sequence = roleInfo.MenuSequence };
                                menus.Add(menu);
                            }

                            var menuItem = menu.MenuItems.FirstOrDefault(c => c.Title == roleInfo.MenuItemTitle);
                            if (menuItem == null)
                            {
                                menuItem = new MenuItem
                                {
                                    Area = "Manage",
                                    Title = roleInfo.MenuItemTitle,
                                    Controller = roleInfo.MenuItemController,
                                    Action = roleInfo.MenuItemAction,
                                    Sequence = roleInfo.MenuItemSequence
                                };
                                menu.MenuItems.Add(menuItem);
                            }
                        }
                    }

                    controller.ViewBag.Menus = menus;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}

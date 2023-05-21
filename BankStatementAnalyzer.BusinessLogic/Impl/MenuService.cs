using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.BusinessLogic.ViewModels;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMemoryCacheService memoryCacheService;
        private readonly IPermissionMasterService permissionMasterService;

        public MenuService(IMenuRepository menuRepository,
            IMemoryCacheService memoryCacheService,
            IPermissionMasterService permissionMasterService)
        {
            this.menuRepository = menuRepository;
            this.memoryCacheService = memoryCacheService;
            this.permissionMasterService = permissionMasterService;
        }

        public void Add(Menu entity)
        {
            menuRepository.Add(entity);
        }

        public void Delete(Menu entity)
        {
            menuRepository.Delete(entity);
        }

        public void Edit(Menu entity)
        {
            menuRepository.Edit(entity);
        }

        public IQueryable<Menu> FindBy(Expression<Func<Menu, bool>> predicate)
        {
            return menuRepository.FindBy(predicate);
        }

        public IQueryable<Menu> GetAll()
        {
            return menuRepository.GetAll();
        }

        public IQueryable<Menu> GetAllFirstLevel()
        {
            return menuRepository.FindBy(m => m.Parent == null);
        }

        public List<Menu> GetAllForUser(string username, bool isSystemAdmin)
        {
            return memoryCacheService.GetOrSet(MemoryCacheKeys.KEY_MENUS + username, () => getAllForUser(username, isSystemAdmin));
        }

        private List<Menu> getAllForUser(string username, bool isSystemAdmin)
        {
            List<Menu> menus = menuRepository.FindBy(m => m.Parent == null).OrderBy(m => m.Order).ToList();
            if (isSystemAdmin)
            {
                menus = menus.Where(m => !m.HideFromSystemAdministrator).ToList();
            }

            List<string> permissions = permissionMasterService.GetAllForUser(username);

            for (int i = menus.Count - 1; i >= 0; i--)
            {
                if (GetRoute(menus[i].Controller, menus[i].ActionName) != null)
                {
                    if (!permissions.Contains(GetRoute(menus[i].Controller, menus[i].ActionName)))
                        menus.RemoveAt(i);
                }
                else
                {
                    var m = menus[i];
                    List<Menu> childMenus = menuRepository.GetAll().Where(x => x.ParentId == m.ID).ToList();

                    for (int j = childMenus.Count - 1; j >= 0; j--)
                    {
                        if (GetRoute(childMenus[j].Controller, childMenus[j].ActionName) != null)
                        {
                            if (!permissions.Contains(GetRoute(childMenus[j].Controller, childMenus[j].ActionName)))
                                menus[i].Children.Remove(childMenus[j]);
                            else
                            {
                                if (!menus[i].Children.Contains(childMenus[j]))
                                    menus[i].Children.Add(childMenus[j]);
                            }
                        }
                        else
                        {
                            if (!menus[i].Children.Contains(childMenus[j]))
                                menus[i].Children.Add(childMenus[j]);
                        }
                    }

                    if (childMenus.Count == 0 || menus[i].Children?.Count == 0)
                        menus.RemoveAt(i);
                }
            }
            return menus;
        }

        private string GetRoute(string controller, string action)
        {
            if (string.IsNullOrWhiteSpace(controller))
                return null;
            else
                return string.Format("{0}.{1}.{2}", "ACTION", controller, action);
        }


        public void Save()
        {
            menuRepository.Save();
        }
    }
}
import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
    templateUrl: './sidebar-nav.component.html',
    selector: 'sidebar-nav',
    encapsulation: ViewEncapsulation.None
})
export class SideBarNavComponent extends AppComponentBase {

    menuItems: MenuItem[] = [
        new MenuItem(this.l('Settings'), '', 'menu', '', [
            new MenuItem(this.l('Users'), 'Pages.Users', 'people', '/app/users'),
            new MenuItem(this.l('Roles'), 'Pages.Roles', 'local_offer', '/app/roles'),
        ]),
        new MenuItem(this.l('Estimates'), '', 'list_alt', '/app/estimates'),

    ];

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
}

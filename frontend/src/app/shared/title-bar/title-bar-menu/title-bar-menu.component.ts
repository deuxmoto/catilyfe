import { Component, Input, Output, OnInit, EventEmitter } from "@angular/core";
import {
  trigger,
  state,
  style,
  animate,
  transition
} from "@angular/animations";

import { TitleBarService } from "../title-bar.service";
import { AuthBackendApi, User, isUserAdmin } from "../../../core/backend-api/auth.backend-api";

interface MenuItem {
    text: string;
    linkUrl: string;
}

@Component({
    selector: "title-bar-menu",
    templateUrl: "./title-bar-menu.component.html",
    styleUrls: [ "./title-bar-menu.component.scss" ],
    animations: [
        trigger("menuBackgroundEnterAnimation", [
            state("in", style({ opacity: 1 })),
            transition("void => *", [
                style({ opacity: 0 }),
                animate("200ms ease")
            ]),
            transition("* => void", [
                animate("200ms ease", style({ opacity: 0 }))
            ])
        ])
    ]
})
export class TitleBarMenuComponent implements OnInit {
    public menuItems: MenuItem[];
    public menuIsOpen = false;
    public loggedInUser: User;

    constructor(
        private authBackendApi: AuthBackendApi,
        private titleBarService: TitleBarService
    ) { }

    public ngOnInit(): void {
        this.menuItems = [
            {
                text: "Home",
                linkUrl: ""
            },
            {
                text: "About",
                linkUrl: "/posts/about"
            },
            {
                text: "Posts",
                linkUrl: "/home"
            }
        ];

        this.authBackendApi.getLoggedInUser().subscribe((user) => {
            this.loggedInUser = user;
        });

        this.titleBarService.getMenuOpen().subscribe((isMenuOpen) => {
            this.menuIsOpen = isMenuOpen;
        });
    }

    public isUserAdmin(): boolean {
        return isUserAdmin(this.loggedInUser);
    }

    public closeMenu(): void {
        this.titleBarService.closeMenu();
    }

    public login(): void {
        this.authBackendApi.gotoLoginPage();
    }

    public logout(): void {
        this.authBackendApi.logout().subscribe(() => {
            console.log("Successfully logged out");
        });
    }
}

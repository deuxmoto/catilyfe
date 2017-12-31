import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import {
  trigger,
  state,
  style,
  animate,
  transition
} from "@angular/animations";

import { TitleBarService } from "./title-bar.service";
import { AuthBackendApi, User } from "../../core/backend-api/auth.backend-api";

interface MenuItem {
    text: string;
    linkUrl: string;
}

export type Theme = "light" | "dark" | "transparent-light" | "transparent-dark";

@Component({
    selector: "title-bar",
    templateUrl: "./title-bar.component.html",
    styleUrls: [ "./title-bar.component.scss" ],
    animations: [
        trigger("fadeInOutAnimation", [
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
export class TitleBarComponent implements OnInit {
    @Input()
    public theme: Theme = "light";

    @Input()
    public showBranding = true;

    public loggedInUser: User;

    constructor(
        private authBackendApi: AuthBackendApi,
        private router: Router,
        private titleBarService: TitleBarService
    ) { }

    public ngOnInit(): void {
        this.authBackendApi.getLoggedInUser().subscribe((user) => {
            this.loggedInUser = user;
        });
    }

    public openMenu(): void {
        this.titleBarService.openMenu();
    }
}

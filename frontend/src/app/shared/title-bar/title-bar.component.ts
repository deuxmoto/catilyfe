import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import {
  trigger,
  state,
  style,
  animate,
  transition
} from "@angular/animations";

interface MenuItem {
    text: string;
    linkUrl: string;
}

@Component({
    selector: "title-bar",
    templateUrl: "./title-bar.component.html",
    styleUrls: [ "./title-bar.component.scss" ],
    animations: [
        trigger("menuEnterAnimation", [
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
    public transparent = false;

    @Input()
    public theme: "light" | "dark" = "light";

    public menuItems: MenuItem[];
    public menuIsOpen = false;

    constructor(private router: Router) {}

    ngOnInit(): void {
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
            },
            {
                text: "Admin",
                linkUrl: "/admin"
            }
        ];
    }

    toggleMenu(): void {
        this.menuIsOpen = !this.menuIsOpen;
    }

    closeMenu(): void {
        this.menuIsOpen = false;
    }
}

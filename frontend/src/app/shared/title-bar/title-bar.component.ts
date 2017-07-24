import { Component, OnInit } from "@angular/core";
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
    private menuItems: MenuItem[];
    private menuIsOpen = false;

    constructor(private router: Router) {}

    ngOnInit(): void {
        this.menuItems = [
            {
                text: "Home",
                linkUrl: "/home"
            },
            {
                text: "About",
                linkUrl: "/home"
            },
            {
                text: "Posts",
                linkUrl: "/home"
            }
        ];
    }

    gotoHome(): void {
        this.router.navigate([""]);
    }

    toggleMenu(): void {
        this.menuIsOpen = !this.menuIsOpen;
    }
}

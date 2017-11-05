import { Component, OnInit, HostListener } from "@angular/core";
import {
  trigger,
  state,
  style,
  animate,
  transition
} from "@angular/animations";

import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/fromEvent";
import "rxjs/add/operator/throttleTime";
import "rxjs/add/operator/delay";

const scrollDelayMs = 500;

@Component({
    selector: "app-testpage",
    templateUrl: "./testpage.component.html",
    styleUrls: [ "./testpage.component.scss" ],
    animations: [
        trigger("titleBarEnterAnimation", [
            state("true", style({ top: "0%" })),
            state("false", style({ top: "-100%" })),
            transition("* => *", [
                animate("500ms ease")
            ])
        ])
    ]
})
export class TestPageComponent implements OnInit {
    public showingTitleBar = true;
    private previousScrollPos: number = null;

    ngOnInit(): void {
        // Listen to scroll events
        Observable.fromEvent(window, "scroll")
            .delay(scrollDelayMs / 2)
            .throttleTime(scrollDelayMs / 2)
            .subscribe(this.onScroll.bind(this));
    }

    onScroll(): void {
        const previousScrollPosition = this.previousScrollPos;
        const currentScrollPosition = window.scrollY;
        console.log(currentScrollPosition);

        this.showingTitleBar = (previousScrollPosition === null) || (this.previousScrollPos > currentScrollPosition);
        this.previousScrollPos = currentScrollPosition;
    }
}

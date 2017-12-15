import { Injectable } from "@angular/core";

import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";

@Injectable()
export class TitleBarService {
    private _titleBarMenuOpen = new Subject<boolean>();

    public openMenu(): void {
        this._titleBarMenuOpen.next(true);
    }

    public closeMenu(): void {
        this._titleBarMenuOpen.next(false);
    }

    public getMenuOpen(): Observable<boolean> {
        return this._titleBarMenuOpen.asObservable();
    }
}

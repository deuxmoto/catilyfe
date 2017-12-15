import { Component, OnInit, Input } from "@angular/core";

import { BackendApiService, MarkdownPreview } from "../../../core/backend-api.service";

enum State {
    Normal,
    Loading
}

@Component({
    selector: "markdown-preview",
    templateUrl: "./markdown-preview.component.html"
})
export class MarkdownPreviewComponent implements OnInit {
    public StateEnum = State;
    public state = State.Loading;
    public htmlPreview: string;

    @Input()
    public markdown: string;

    constructor(
        private backend: BackendApiService
    ) { }

    public ngOnInit(): void {
        this.state = State.Loading;
        this.backend.getMarkdownPreview(this.markdown)
            .subscribe((markdownPreview) => {
                this.htmlPreview = markdownPreview.content;
                this.state = State.Normal;
            });
    }
}

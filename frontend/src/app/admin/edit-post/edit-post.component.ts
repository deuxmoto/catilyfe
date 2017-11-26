import { Component, OnInit, OnDestroy, ViewChild } from "@angular/core";
import { FormControl, FormGroup, Validators, ControlValueAccessor } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { DataSource } from "@angular/cdk/collections";

import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/fromEvent";
import "rxjs/add/operator/map";

import { MarkdownPreviewComponent } from "./markdown-preview/markdown-preview.component";
import {
    BackendApiService, AdminPost, AdminPostMetadata,
    NotFoundError, UnknownError
} from "../../core/backend-api.service";

enum State {
    Normal,
    Loading,
    Saving,
    Error
}

interface DisplayTab {
    text: string;
    value: Tab;
}
enum Tab {
    Metadata,
    Markdown,
    MarkdownPreview
}

@Component({
    selector: "edit-post",
    templateUrl: "./edit-post.component.html",
    styleUrls: [ "./edit-post.component.scss" ]
})
export class EditPostComponent implements OnInit {
    public metadataForm: FormGroup;

    // The following fields are either not included in the above metadataForm
    // or are duplicated, because they're being used in parts of the UI that can't
    // read from or worth with a FormGroup easily:
    public content: string;
    public tags: string[];

    public state = State.Loading;
    public StateEnum = State;
    public lastErrorMessage: string;
    public savingText: string;

    public TabEnum = Tab;
    public tabs: DisplayTab[] = [
        {
            text: "Metadata",
            value: Tab.Metadata
        },
        {
            text: "Markdown",
            value: Tab.Markdown
        },
        {
            text: "Markdown Preview",
            value: Tab.MarkdownPreview
        }
    ];
    public currentTab = Tab.Metadata;

    constructor(
        private backend: BackendApiService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    public ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get("id");
        if (!id) {
            console.log("Yeah you can try creating a post, but it ain't gonna work");
            return;
        }

        // Create the form controls
        this.metadataForm = new FormGroup({
            id: new FormControl({ value: "", disabled: true }),
            title: new FormControl(""),
            description: new FormControl(""),
            slug: new FormControl(""),
            whenCreated: new FormControl({ value: "", disabled: true }),
            whenPublished: new FormControl(""),
            newTag: new FormControl("")
        });

        this.backend.getAdminPost(id).subscribe(
            (post) => {
                const metadata = post.metadata;
                this.metadataForm.reset({
                    id: metadata.id,
                    title: metadata.title,
                    description: metadata.description,
                    slug: metadata.slug,
                    whenCreated: metadata.whenCreated,
                    whenPublished: metadata.whenPublished
                });
                this.tags = metadata.tags;
                this.content = post.markdownContent;

                this.state = State.Normal;
            },
            (error) => {
                this.handleNetworkError(error);
            }
        );
    }

    public closeEditPost(): void {
        this.router.navigate([ "../../" ], { relativeTo: this.route });
    }

    public savePost(): void {
        const formMetadata = this.metadataForm;
        const adminPost: AdminPost = {
            metadata: {
                id: formMetadata.get("id").value,
                title: formMetadata.get("title").value,
                description: formMetadata.get("description").value,
                slug: formMetadata.get("slug").value,
                whenCreated: formMetadata.get("whenCreated").value,
                whenPublished: formMetadata.get("whenPublished").value,
                tags: this.tags
            },
            markdownContent: this.content
        };

        this.state = State.Saving;
        this.savingText = "Saving...";
        this.backend.setAdminPost(adminPost).subscribe(
            () => {
                this.savingText = "Saved! Yeeee";
                this.state = State.Normal;
            },
            (error) => {
                this.handleNetworkError(error);
            }
        );
    }

    public setCurrentTab(tab: Tab): void {
        this.currentTab = tab;
    }

    public addTag(keypress?: KeyboardEvent):void {
        // Only add tag for Enter key
        if (keypress && keypress.keyCode !== 13) {
            return;
        }

        const newTagTextBox = this.metadataForm.get("newTag");
        this.tags.push(newTagTextBox.value);
        newTagTextBox.reset("");
    }

    private handleNetworkError(error: any): void {
        if (error instanceof NotFoundError) {
            this.router.navigateByUrl("notfound", { skipLocationChange: true });
        }
        else if (error instanceof UnknownError) {
            this.lastErrorMessage = error.errorMessage;
        }

        this.state = State.Error;
    }
}

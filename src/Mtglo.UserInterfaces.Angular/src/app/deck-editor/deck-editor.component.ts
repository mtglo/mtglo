import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm, FormGroup, FormControl, Validators } from '@angular/forms'

@Component({
    selector: 'mtglo-deck-editor',
    templateUrl: './deck-editor.component.html',
    styleUrls: ['./deck-editor.component.css']
})
export class DeckEditorComponent implements OnInit {

    genders = ['male', 'female'];
    cardForm: FormGroup;
    editions =['Alpha', 'Beta', 'Unlimited', 'Revised', 'The Dark', 'Legends'];

    constructor() { }

    ngOnInit(): void {
        this.cardForm = new FormGroup({
            'cardName': new FormControl('Lightning Bolt', Validators.required,),
            'quantity': new FormControl(4, [Validators.required, Validators.min(1), Validators.max(4)]),
            'edition': new FormControl()
        });

    }

    OnSubmit() {
        console.log(this.cardForm);
    }

}

import { Component, OnInit, OnDestroy } from '@angular/core';
import { DeckService, } from '../deck.service';
import { Deck } from '../deck.model'
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { DeckLibraryService } from '../deck-library.service';


@Component({
    selector: 'mtglo-deck-library',
    templateUrl: './deck-library.component.html',
    styleUrls: ['./deck-library.component.css']
})
export class DeckLibraryComponent implements OnInit, OnDestroy {

    constructor(public deckLibraryService: DeckLibraryService, public deckService: DeckService) {

    }

    decks: Deck[];
    deckLibaryServiceSub: Subscription;

    ngOnInit(): void {
        this.deckLibaryServiceSub = this.deckLibraryService.FetchDecks().subscribe((decks: Deck[]) => this.decks = decks);
    }

    ngOnDestroy() {
        this.deckLibaryServiceSub.unsubscribe();
    }

}

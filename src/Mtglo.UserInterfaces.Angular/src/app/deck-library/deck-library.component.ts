import { Component, OnInit, Output } from '@angular/core';

@Component({
  selector: 'mtglo-deck-library',
  templateUrl: './deck-library.component.html',
  styleUrls: ['./deck-library.component.css']
})
export class DeckLibraryComponent implements OnInit {

  listOfDecks = [{deckName: 'Burn'}, {deckName: 'Affinity'}, {deckName: 'Death_and_Taxes'}, {deckName: 'Blue_is_Dumb'}, {deckName: 'Green_Dudes'}];
  constructor() { }

  ngOnInit(): void {
  }

}

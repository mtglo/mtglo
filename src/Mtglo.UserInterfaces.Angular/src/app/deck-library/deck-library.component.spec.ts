import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeckLibraryComponent } from './deck-library.component';

describe('DeckLibraryComponent', () => {
  let component: DeckLibraryComponent;
  let fixture: ComponentFixture<DeckLibraryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeckLibraryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeckLibraryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DecklistComponent } from './decklist.component';

describe('DecklistComponent', () => {
  let component: DecklistComponent;
  let fixture: ComponentFixture<DecklistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DecklistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DecklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

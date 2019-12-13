import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import * as spaceReducer from '../spaces/state/space.reducers';
import * as spaceActions from '../spaces/state/space.actions';
import * as spaceSelectors from '../spaces/state/space.selector';
import { takeWhile } from 'rxjs/operators';
import { SpaceQueryResult } from '../spaces/models/spaceQueryResult';
@Component({
  selector: 'app-map-space',
  templateUrl: './map-space.component.html',
  styleUrls: ['./map-space.component.scss']
})
export class MapSpaceComponent implements OnInit {
  
  componentActive = true;
  spaceQueryResult = <SpaceQueryResult>{items: [], totalItems: 0};
  type: string;
  searchString: string;
  currentPage: 1
  query = {
    currentPage: null,
    pageSize: null,
    spaceType: null,
    searchString: null,
    size: null
  };
  capacities = [
    {label: 'Up to 50', capacity: 50},
    {label: 'Up to 100', capacity: 100},
    {label: 'Up to 200', capacity: 199},
    {label: 'Up to 500', capacity: 499},
    {label: 'Up to 1000', capacity: 1000},
    {label: 'More than 1000', capacity: 1000}
  ];
  selectedCapacity: any;
  constructor(private store: Store<spaceReducer.SpaceState>,
              private route: ActivatedRoute){
  }
  
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.type = params['query']? params['query'] : '';
        this.query = {currentPage: 1, pageSize: 1, ...this.route.params['value']};
      this.store.dispatch(new spaceActions.GetSpaces(this.query));
    });

    this.store.pipe(select(spaceSelectors.getSpaceQueryResult),
    takeWhile(() => this.componentActive))
    .subscribe(spaceQR => {
      this.spaceQueryResult = spaceQR
    });
    console.log(this.selectedCapacity);
    
  }

  onPageChange(page: number) {
    this.query = { ...this.query, currentPage: page, searchString: this.searchString, size: this.selectedCapacity };
    this.store.dispatch(new spaceActions.GetSpaces(this.query));

    this.store.pipe(select(spaceSelectors.getSpaceQueryResult),
    takeWhile(() => this.componentActive))
    .subscribe(spaceQR => {
      this.spaceQueryResult = spaceQR
    });
  }

  search() {
    this.query = { ...this.query, searchString: this.searchString, size: this.selectedCapacity };
    console.log(this.query);
    this.store.dispatch(new spaceActions.GetSpaces(this.query));

    this.store.pipe(select(spaceSelectors.getSpaceQueryResult),
    takeWhile(() => this.componentActive))
    .subscribe(spaceQR => {
      this.spaceQueryResult = spaceQR
    });
  }
}

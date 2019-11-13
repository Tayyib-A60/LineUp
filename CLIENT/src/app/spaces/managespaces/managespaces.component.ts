import { Component, OnInit, ViewChild } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable/release'

import * as spaceActions from './../state/space.actions';
import * as spaceReducer from './../state/space.reducers';
import * as spaceSelectors from './../state/space.selector';
import { Store, select } from '@ngrx/store';
import { takeWhile } from 'rxjs/operators';
import { SpaceQueryResult } from '../models/spaceQueryResult';

@Component({
  selector: 'app-managespaces',
  templateUrl: './managespaces.component.html',
  styleUrls: ['./managespaces.component.scss']
})
export class ManagespacesComponent implements OnInit {

  @ViewChild(DatatableComponent, {static: false}) table: DatatableComponent;
  spaceQueryResult: SpaceQueryResult;
  componentActive = true;
  
    constructor(private store: Store<spaceReducer.SpaceState>) {
    }

    ngOnInit() {
      this.store.dispatch(new spaceActions.GetMerchantSpaces());

      this.store.pipe(select(spaceSelectors.getMerchantSpaces),
      takeWhile(() => this.componentActive))
      .subscribe(spaceQR => {
        this.spaceQueryResult = spaceQR
        console.log(this.spaceQueryResult);
      });
    }

    deleteSpace(id: number) {
      this.store.dispatch(new spaceActions.DeleteSpace(id));
    }

}

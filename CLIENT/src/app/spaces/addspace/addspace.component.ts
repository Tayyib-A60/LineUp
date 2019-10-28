import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { Store, select } from '@ngrx/store';
import * as spaceReducer from '../state/space.reducers';
import * as spaceActions from '../state/space.actions';
import * as spaceSelectors from '../state/space.selector';
import { Params, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SpaceType } from '../models/spaceType.model';
import { takeWhile } from 'rxjs/operators';
import { Space } from '../models/space.model';

@Component({
  selector: 'app-addspace',
  templateUrl: './addspace.component.html',
  styleUrls: ['./addspace.component.scss']
})
export class AddspaceComponent implements OnInit {
  amenityItems = [ ];
  selectedSpaceType = '';
  spaceForm: FormGroup;
  id: string;
  editMode: boolean;
  spaceTypes$: Observable<SpaceType[]>;
  componentActive = true;
  spaceTypes: SpaceType[];
  spaceToEdit: Space;
    constructor(private formBuilder: FormBuilder,
                private store: Store<spaceReducer.SpaceState>,
                private route: ActivatedRoute) { }
                
    ngOnInit() {
      this.route.params
      .subscribe(
        (params: Params) => {
          this.id = params['id'];
          this.editMode = params['id'] != null;
          this.initializeForm();
        }
      );
      this.store.dispatch(new spaceActions.GetSpaceTypes());

      // this.spaceTypes$ = this.store.pipe(select(spaceSelectors.getSpaceTypes));
      this.store.pipe(select(spaceSelectors.getSpaceTypes),
      takeWhile(() => this.componentActive))
      .subscribe(spaceTypes => {
        this.spaceTypes = spaceTypes;
      });
    }

    ngOnDestroy(): void {
      this.componentActive = false;
    }

    private initializeForm() {
      let amenitiesArray = new FormArray([]);
      let name = '';
      let location = '';
      let size = null;
      let type = {type: '', id: null};
      let st = '';
      let stid = null;
      let description =  '';
      let price = null;
      let userId = 3;
      if(this.editMode) {
        // fetch the space to edit nd initialize the form to contain it's properties here.
        this.store.dispatch(new spaceActions.GetSingleSpace(Number(this.id)));
        this.store.pipe(select(spaceSelectors.getSingleSpace),
        takeWhile(() => this.componentActive))
          .subscribe(space => {
            this.spaceToEdit = space;
            name = space.name;
            location = space.location;
            description = space.description;
            size = space.size;
            price = space.price;
            type = space.type;
            if(space.type) {
              st = space.type.type;
              stid = space.type.id;
            }
            this.selectedSpaceType = st;
            const amenities = space.amenities;
            // console.log(amenities);
          
              if(amenities) {
                console.log('Got here', amenities.length);
                
                  amenities.map(a => {
                    amenitiesArray.push(
                      new FormGroup({
                        'name': new FormControl(a['name'], Validators.required),
                        'price': new FormControl(a['price'], Validators.required)
                      })
                    );
                  }); 
              } 
            
            this.spaceForm = new FormGroup({
              'id': new FormControl(this.id, Validators.required),
              'name': new FormControl(name, [Validators.required,  Validators.minLength(3),
                                            Validators.maxLength(50)]),
              'location': new FormControl(location, Validators.required),
              'description': new FormControl(description, Validators.required),
              'size': new FormControl(size, Validators.required),
              'price': new FormControl(price, Validators.required),
              'amenities': amenitiesArray,
              'userId': new FormControl(userId),
              'type': new FormGroup({
                'type': new FormControl(st),
                'id': new FormControl(stid)
              })
            });

        });
        
      }
      // this.spaceForm = this.formBuilder.group({
      //   spaceName: ['', [Validators.required,
      //         Validators.minLength(3),
      //         Validators.maxLength(50)]],
      //   location: ['', Validators.required],
      //   size: '',
      //   description: '',
      //   price: '',
      //   amenities
      // });      
      
      this.spaceForm = new FormGroup({
        'name': new FormControl(name, [Validators.required,  Validators.minLength(3),
                                      Validators.maxLength(50)]),
        'location': new FormControl(location, Validators.required),
        'description': new FormControl(description, Validators.required),
        'size': new FormControl(size, Validators.required),
        'price': new FormControl(price, Validators.required),
        'amenities': amenitiesArray,
        'userId': new FormControl(userId),
        'type': new FormGroup({
          'type': new FormControl(null),
          'id': new FormControl(null)
        })
      });
    }

    addAmenity() {
      (<FormArray>this.spaceForm.get('amenities')).push(
        new FormGroup({
          'name': new FormControl(null, Validators.required),
          'price': new FormControl(null, Validators.required)
        })
      );      
    }
    
    removeAmenity(index: number) {
      (<FormArray>this.spaceForm.get('amenities')).removeAt(index);
    }

    toggleCheckbox(item) {
      if(item.currentTarget.checked) {
        this.amenityItems.push(item.currentTarget.id);
      } else {
        this.amenityItems.splice((this.amenityItems.indexOf(item.currentTarget.id),1))
      }
    }

    createSpace() {
      const spaceTypeIndex = this.spaceTypes.findIndex(c => c.type === this.selectedSpaceType);
      const spaceTypeId = this.spaceTypes[spaceTypeIndex].id;
      this.spaceForm.get('type').patchValue({
        type: this.selectedSpaceType,
        id: spaceTypeId
      });
      if(this.editMode) {
        console.log(this.spaceForm.value);
        
        this.store.dispatch(new spaceActions.UpdateSpace(this.spaceForm.value));
      }
      else {
        this.store.dispatch(new spaceActions.CreateSpace(this.spaceForm.value));
      }
      // console.log(this.spaceForm.value);
      this.spaceForm.reset();
      this.selectedSpaceType = '';  
    }

}

import { UserState } from './user.reducers';
import { SpaceState } from '../spaces/state/space.reducers';

export interface AppState {
    user: UserState,
    spaces: SpaceState
};
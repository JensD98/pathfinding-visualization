import { createStore, Module, createLogger } from 'vuex';
import {
  GridModuleState,
  gridModuleFactory
} from './modules/gridModule/gridModule';

export type RootState = {
  gridModule: GridModuleState;
};

export const store = createStore({
  modules: {
    gridModule: gridModuleFactory({
      INIT_ROWS: 5,
      INIT_COLS: 10,
      START_POSITION: { row: 2, col: 1 },
      FINISH_POSITION: { row: 2, col: 8 }
    })
  },
  strict: true,
  devtools: true,
  plugins: [
    createLogger({
      actionFilter(action) {
        if (action.type === 'gridModule/nodeOnMouseEnter') {
          return false;
        }
        return true;
      }
    })
  ]
});

export const storeFactory = (gridModule: Module<GridModuleState, RootState>) =>
  createStore({
    modules: {
      gridModule
    }
  });

export default class DragDropModel {
    public index: number|null;

    public row: number|null;
    
    public col: number|null;

    constructor(
        index: number|null,
        row: number|null,
        col: number|null,
    ) {
        this.index = index;
        this.row = row;
        this.col = col;
    }

    updateProperties(newModel: DragDropModel) {
        this.index = newModel.index;
        this.col = newModel.col;
        this.row = newModel.row;
    }

    resetProperties() {
        this.index = null;
        this.row = null;
        this.col = null;
    }
}
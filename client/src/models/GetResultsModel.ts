import PaginationModel from "./PaginationModel";

export default class GetResultsModel extends PaginationModel {
    public userId: number;
    
    constructor(
        userId: number,
        pageNumber: number,
        itemPerPage: number,
    ) {
        super(pageNumber, itemPerPage);
        this.userId = userId;
    }
}
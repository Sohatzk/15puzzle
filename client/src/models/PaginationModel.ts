export default class PaginationModel {
    public pageNumber: number;
    
    public itemsPerPage: number;

    constructor(
        pageNumber: number,
        itemsPerPage: number,
    ) {
        this.pageNumber = pageNumber;
        this.itemsPerPage = itemsPerPage
    }
}
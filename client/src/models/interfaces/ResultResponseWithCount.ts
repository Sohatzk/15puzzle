import ResultModel from "../ResultModel";

export interface ResultResponseWithCount {
    results: ResultModel[];
    totalCount: number;
}
import GetResultsModel from '@/models/GetResultsModel';
import Axios from '../axiosConfig';
import ResultModel from '@/models/ResultModel';
import { ResultResponseWithCount } from '@/models/interfaces/ResultResponseWithCount';

const baseRoute = 'api/results';

export default new (class ResultsService {

    public async getResults(model: GetResultsModel): Promise<ResultResponseWithCount> {
        return Axios.post<ResultResponseWithCount>(`${baseRoute}/GetUserResults`, model)
            .then(((item) => item.data));
    }

    public async addResult(model: ResultModel): Promise<void> {
        return Axios.post<void>(`${baseRoute}/AddResult`, model)
            .then(((item) => item.data));
    }
})();

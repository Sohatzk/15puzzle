import { ResultModel } from '@/models/ResultModel';
import Axios from 'axios';

const baseRoute = '/api/results';

export default new (class ResultsService {
    public async getResults(userId: number): Promise<ResultModel> {
        return Axios.get<ResultModel>(`${baseRoute}`, {
            params: {
                userId
            }
        }).then(((item) => item.data));
    }
})();

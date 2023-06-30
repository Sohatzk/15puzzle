import LogInModel from '@/models/LogInModel';
import { UserModel } from '@/models/interfaces/UserModel';
import Axios from '../axiosConfig';

const baseRoute = 'api/auth';

export default new (class AuthService {

    public async logIn(model: LogInModel): Promise<UserModel> {
        return Axios.post<UserModel>(`${baseRoute}/login`, model)
            .then(((item) => item.data));
    }
})();

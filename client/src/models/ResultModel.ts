export default class ResultModel {
    public MyUserId: number;

    public turns: number;

    public time: string;

    public date: Date;
    
    constructor(
        myUserId: number,
        turns: number,
        time: string,
    ) {
        this.MyUserId = myUserId;
        this.turns = turns;
        this.time = time;
        this.date = new Date();
    }
}
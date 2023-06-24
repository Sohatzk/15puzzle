export default class TimerModel {
    hours: number;
    minutes: number;
    seconds: number;

    constructor(
        hours: number,
        minutes: number,
        seconds: number,
    ) {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }

    reset() {
        this.hours = 0;
        this.minutes = 0;
        this.seconds = 0;
    }
}
import { useEffect, useState } from "react"


const secondsToTime = (secs) => {
    const hours = Math.floor(secs / (60 * 60));

    const divisor_for_minutes = secs % (60 * 60);
    const minutes = Math.floor(divisor_for_minutes / 60);

    const divisor_for_seconds = divisor_for_minutes % 60;
    const seconds = Math.ceil(divisor_for_seconds);

    return {
        "h": hours,
        "m": minutes,
        "s": seconds
    };
}

export const useTimer = (secondsToCount = 0, onTimeOut = () => { }) => {
    const [timerState, setTimerState] = useState({
        seconds: secondsToCount,
        time: secondsToTime(secondsToCount)
    });
    const [isActive, setIsActive] = useState(false);


    const startTimer = () => {
        setIsActive(true);
    }

    const clearTimer = () => {
        setIsActive(false);
    }

    useEffect(() => {
        let interval = null;
        if (timerState.seconds === 0) {
            clearTimer();
            onTimeOut();
            return;
        }
        if (isActive) {
            interval = setInterval(() => {
                setTimerState(prev => {
                    const secondsLeft = prev.seconds - 1;
                    return {
                        time: secondsToTime(secondsLeft),
                        seconds: secondsLeft,
                    }
                });
            }, 1000);
        } else if (!isActive && timerState.seconds !== 0) {
            clearInterval(interval);
        }
        return () => clearInterval(interval);
    }, [isActive, timerState])

    return [timerState, startTimer, clearTimer];
}
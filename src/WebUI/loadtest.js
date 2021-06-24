import http from 'k6/http';
import {check, sleep} from 'k6';

export let options = {
    stages: [
        { duration: '2m', target: 400 }, // Increasing from 1 to 400 Virtual Users (VUs) over 2 minutes.
        { duration: '3m', target: 400 }, // Staying at 400 VUs for 3 minutes.
        { duration: '1m', target: 0 },   // Decrease to 0 VUs over 1 minute.
    ],    
    thresholds: {
        http_req_failed: ['rate<0.05'],   // http errors should be less than 5%
        http_req_duration: ['p(90)<400'], // 90% of requests should be below 400ms
        checks: ['rate>0.9'],             // the rate of successful checks should be higher than 90%
    },
}

export default function(){
    let res = http.get('http://172.17.0.4:80/api/authoring/1');
    check(res,{'status was 200': r => r.status == 200});
    sleep(1);
}
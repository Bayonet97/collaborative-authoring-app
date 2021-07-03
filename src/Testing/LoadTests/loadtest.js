import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    stages: [
        { duration: '2m', target: 200 }, // Increasing from 1 to 200 Virtual Users (VUs) over 2 minutes.
        { duration: '4m', target: 200 }, // Staying at 200 VUs for 4 minutes.
        { duration: '1m', target: 0 },   // Decrease to 0 VUs over 1 minute.
    ],    
    thresholds: {
        http_req_failed: ['rate<0.05'],   // http errors should be less than 5%
        http_req_duration: ['p(90)<400'], // 90% of requests should be below 400ms
        checks: ['rate>0.9'],             // the rate of successful checks should be higher than 90%
    },
}

export default function () {
    var url = 'http://146.148.121.174/api/authoring/author';
    var payload = JSON.stringify({
        "UserId": "21e247c6-5b1e-4c3b-947e-ea21fe0644be",
        "BookId": "910a9dff-7b21-46c6-9e80-090793cf1d0f",
        "PageId": "c74f8e17-40ab-441b-aa27-6bd8dde8b61a",
        "PageChangeType": 0,
        "Letters": "a",
        "Position": 0,
        "Amount": 0
    });
    var params = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjhiMjFkNWE1Y2U2OGM1MjNlZTc0MzI5YjQ3ZDg0NGE3YmZjODRjZmYiLCJ0eXAiOiJKV1QifQ.eyJuYW1lIjoiUmFwaGHDq2wiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDMuZ29vZ2xldXNlcmNvbnRlbnQuY29tL2EtL0FPaDE0R2lQUEZZbHhMQ3RSZTRYVDctZnBpM2JKMVdfenZnNWNkdWlwcTB1PXM5Ni1jIiwiSWQiOiIyMWUyNDdjNi01YjFlLTRjM2ItOTQ3ZS1lYTIxZmUwNjQ0YmUiLCJ1c2VyIjp0cnVlLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiaXNzIjoiaHR0cHM6Ly9zZWN1cmV0b2tlbi5nb29nbGUuY29tL2NvbGxhYm9yYXRpdmUtYXV0aG9yaW5nIiwiYXVkIjoiY29sbGFib3JhdGl2ZS1hdXRob3JpbmciLCJhdXRoX3RpbWUiOjE2MjUyMjU3NDEsInVzZXJfaWQiOiJYMndiaXVuMWZCWW41cWFFa0paZUZBOEVqMUkzIiwic3ViIjoiWDJ3Yml1bjFmQlluNXFhRWtKWmVGQThFajFJMyIsImlhdCI6MTYyNTIyNTc0MSwiZXhwIjoxNjI1MjI5MzQxLCJlbWFpbCI6InJhcGhhZWx2YWtrZXJ2ZWtlbkBob3RtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJmaXJlYmFzZSI6eyJpZGVudGl0aWVzIjp7Imdvb2dsZS5jb20iOlsiMTE4MzExMTA0NzI1MTY1NzQwMTc1Il0sImVtYWlsIjpbInJhcGhhZWx2YWtrZXJ2ZWtlbkBob3RtYWlsLmNvbSJdfSwic2lnbl9pbl9wcm92aWRlciI6Imdvb2dsZS5jb20ifX0.asZif5TtwLZhSqjhmMhb_OnIg5RF1Ji81SSt6UOYsKEnOJvCoIAUfCxJ1R-4ArxxibDJOmJ3VaqNG14gNU8jsFuO1_avPOFBrGAPBLNZE2Nt07tH_-1C24t5nrK1qPreyuDSvnMcBe8nTK_vVDNJUsIDWStInHcNQE-0fxLMz5uXK3_ZP0YzbBMtbHWu7r0u5f248DovEaY6JPidzkiUtpDrGEi5sCdLkZJZqTgds5MI-4gI7H3I0CAnZb2rBz1k5Q10aXRxqvlzSK7vRiM4Fmp5qNxxUYhxt5m_WYiUwbRA6Unv06wLVMyQBj4Mfs-obStfxLRs36KOJEUvpy4D4A'    
        }
    }
    let res = http.post(url, payload, params);
    check(res,{'status was 200': r => r.status == 200});
    sleep(1);
}
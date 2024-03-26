// Function to fetch data from the API
async function fetchLocationData(l: number,x: number,y: number): Promise<LocationResponseData> {
  const response = await fetch('http://100.115.92.204:5260/location/${l}/${x}/${y}');
  if (!response.ok) {
    throw new Error('Failed to fetch data');
  }
  const data = await response.json();
  const responseData: LocationResponseData = JSON.parse(data);
  return responseData;
}



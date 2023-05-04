
    export const getData = async function(url){
        const response = await fetch(url);
        if(!response.ok)
            errorHandler(response);
        return await response.json();
        };
    
    export const postData = async function(url, body){
      var result = JSON.stringify(body);
      console.log(result);
      const response = await fetch(url,{
        method: 'POST',
        headers: {"Content-type":'application/json'},
        body : JSON.stringify(body)
      });
     if(!response.ok)
     console.log(response.error)
       // errorHandler(response);
    }    

    function errorHandler(response){
        const error = new Error(response.message || 'Failed to fetch');
        throw error;
    }


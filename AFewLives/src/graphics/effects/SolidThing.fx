float colorDrain;
sampler tex : register(s0);

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 texCoord : TEXCOORD0;
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR
{
    float4 col = tex2D(tex, input.texCoord) * input.Color;
    float val = (col.r + col.g + col.b) / 3;
    float4 bw = float4(val, val, val, col.a);
    return lerp(col, bw, colorDrain);
}

technique SolidThing
{
    pass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}



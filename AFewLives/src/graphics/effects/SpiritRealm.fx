float visibility;
sampler tex : register(s0);

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 texCoord : TEXCOORD0;
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR
{
    float4 col = tex2D(tex, input.texCoord);
    col.a = visibility;
    return col;
}

technique SpiritRealm
{
    pass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}



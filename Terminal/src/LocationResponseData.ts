interface LocationResponseData {
    pairGroups: PairGroup[];
  }
  
  interface PairGroup {
    current: CurrentData;
    north?: DirectionData;
    south?: DirectionData;
    east?: DirectionData;
    west?: DirectionData;
  }
  
  interface CurrentData {
    x: number;
    y: number;
    topType: number;
    bottomType: number;
    topTrigram: TrigramData;
    bottomTrigram: TrigramData;
    description: string;
    magnitude: number;
    maxMagnitude: number;
    pressure: number;
    layer: number;
    stability: number;
  }
  
  interface DirectionData {
    x: number;
    y: number;
    topType: number;
    bottomType: number;
    topTrigram: TrigramData;
    bottomTrigram: TrigramData;
    description: string;
    magnitude: number;
    maxMagnitude: number;
    pressure: number;
    layer: number;
    stability: number;
  }
  
  interface TrigramData {
    domain: number;
    type: number;
    frequency: number;
    color: ColorData;
    character: string;
    description: string;
  }
  
  interface ColorData {
    r: number;
    g: number;
    b: number;
    a: number;
    isKnownColor: boolean;
    isEmpty: boolean;
    isNamedColor: boolean;
    isSystemColor: boolean;
    name: string;
  }
  